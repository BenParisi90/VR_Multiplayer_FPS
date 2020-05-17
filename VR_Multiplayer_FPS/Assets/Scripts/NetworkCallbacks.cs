using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{

    public override void SceneLoadLocalDone(string map)
    {
    }

    public override void SceneLoadRemoteDone(BoltConnection connection)
    {
    }
}