using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using TMPro;

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
                _model.scoreDidChange -= ScoreDidChange;
            }

            // Store the model
            _model = value;

            Debug.Log("set _model with " + _realtime.clientID);

            if (_model != null) {

                // Register for events so we'll know if the color changes later
                _model.scoreDidChange += ScoreDidChange;
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

    void ScoreDidChange(GameModel model, int value)
    {
        UpdateScoreText();
    }

    void InitializeGame()
    {
        Debug.Log("Initialize Game");
        GameObject target = Realtime.Instantiate(targetPrefab.name, new Vector3(), Quaternion.identity, false);
        UpdateScoreText();
    }

    public void ChangeScore(int amount)
    {
        _model.score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = _model.score.ToString();
    }
}