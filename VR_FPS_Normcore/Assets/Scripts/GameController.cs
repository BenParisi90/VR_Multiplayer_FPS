using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class GameController : RealtimeComponent {

    private GameModel _model;
    public GameObject player;

    public List<Transform> positionTransforms;


    private GameModel model {
        set {
            // Store the model
            _model = value;
            MoveToNextEmptyPosition();
        }
    }

    public void MoveToNextEmptyPosition() 
    {
        if(_model.spot1Player == 0)
        {
            player.transform.position = positionTransforms[0].position;
            _model.spot1Player = realtime.clientID;
        }
        else if(_model.spot2Player == 0)
        {
            player.transform.position = positionTransforms[1].position;
            _model.spot2Player = realtime.clientID;
        }
    }
}