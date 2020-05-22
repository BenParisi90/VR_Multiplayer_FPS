using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : RealtimeComponent {

    public static GameController instance;

    private GameModel _model;

    public List<Transform> positionTransforms;

    private Realtime _realtime;
    public Transform player;
    public GameObject targetPrefab;

    public TextMeshPro scoreText;

    void Awake() {
        _realtime = GetComponent<Realtime>();
        _realtime.didConnectToRoom += DidConnectToRoom;
    }

    void Start() {
        instance = this;
        Debug.Log("Start with " + _realtime.clientID);
    }


    private GameModel model {
        set 
        {
            if (_model != null) {
                // Unregister from events
                foreach(PlayerModel playerModel in _model.players)
                {
                    playerModel.scoreDidChange -= ScoreDidChange;
                }
            }

            // Store the model
            _model = value;

            Debug.Log("set _model with " + _realtime.clientID);

            if (_model != null) {

                if(_model.players.Count < 2)
                {
                    for(int i = 0; i < 2; i ++)
                    {
                        _model.players.Add(new PlayerModel());
                    }
                }
                foreach(PlayerModel playerModel in _model.players)
                {
                    playerModel.scoreDidChange += ScoreDidChange;
                }
                // Register for events so we'll know if the color changes later
            }
        }   
    }

    void Update()
    {
        if(_realtime != null)
        {
            //button for disconnecting
            if(OVRInput.Get(OVRInput.Button.One) && _realtime.connected)
            {
                _realtime.Disconnect();
            }
            //if we are not connected, can be connected, automatically do so
            else if(_realtime.disconnected && !_realtime.connecting)
            {
                Debug.Log("Disconnected, reconnecting");
                _realtime.Connect("Test Room");
            }
        }
    }

    void OnDestroy() {
        _realtime.didConnectToRoom -= DidConnectToRoom;
    }

    void DidConnectToRoom(Realtime room) {
        if (!gameObject.activeInHierarchy || !enabled)
            return;

        Debug.Log("DidConnectToRoom with " + _realtime.clientID);
        player.position = positionTransforms[_realtime.clientID].position;

        if(!_model.gameInitialized)
        {
            InitializeGame();
            _model.gameInitialized = true;
        }
        UpdateScoreText();
    }

    void InitializeGame()
    {
        Debug.Log("Initialize Game");
        UpdateScoreText();
    }

    public void ChangeScore(int amount)
    {
        _model.players[_realtime.clientID].score += amount;
        Debug.Log("ChangeScore" + _realtime.clientID + ", new score = " + _model.players[_realtime.clientID].score);
    }

    void ScoreDidChange(PlayerModel model, int value)
    {
        Debug.Log("Score did change " + value);
        UpdateScoreText();
        if(value == 3)
        {
            SceneManager.LoadScene("Level1");
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "";
        RealtimeAvatarManager avatarManager = GetComponent<RealtimeAvatarManager>(); 
        Debug.Log("Update score text");
        for(int i = 0; i < _model.players.Count; i ++){    
            Debug.Log("Score: " + i + " - " + _model.players[i].score );
            scoreText.text += "/" + _model.players[i].score;
        }
    }
}