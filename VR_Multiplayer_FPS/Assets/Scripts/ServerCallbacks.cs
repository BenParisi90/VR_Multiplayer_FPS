using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class ServerCallbacks : Bolt.GlobalEventListener
{

    void Awake()
    {
        WeaponControllerObjectRegestry.CreateServerWeapon();
    }

    public override void Connected(BoltConnection connection)
    {
        var log = LogEvent.Create();
        log.Message = string.Format("{0} connected", connection.RemoteEndPoint);
        log.Send();

        WeaponControllerObjectRegestry.CreateClientWeapon(connection);
    }

    public override void Disconnected(BoltConnection connection)
    {
        var log = LogEvent.Create();
        log.Message = string.Format("{0} disconnected", connection.RemoteEndPoint);
        log.Send();
    }

    public override void SceneLoadLocalDone(string map)
    {
        WeaponControllerObjectRegestry.ServerWeapon.Spawn();
    }

    public override void SceneLoadRemoteDone(BoltConnection connection)
    {
        WeaponControllerObjectRegestry.GetWeapon(connection).Spawn();
    }
}