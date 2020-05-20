using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class GameController : RealtimeComponent {

    private GameModel _model;

    public List<Transform> positionTransforms;

    private Realtime _realtime;

    void Awake() {
        _realtime = GetComponent<Realtime>();
        _realtime.didConnectToRoom += DidConnectToRoom;
    }

    void Start() {
        Debug.Log("Start with " + _realtime.clientID);
    }


    private GameModel model {
        set 
        {
            // Store the model
            _model = value;
            Debug.Log("set _model with " + _realtime.clientID);
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
    }
}