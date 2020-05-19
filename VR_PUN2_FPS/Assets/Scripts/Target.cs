using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Target : MonoBehaviourPunCallbacks
{
    public void TakeDamage()
    {
        photonView.RPC("DestroyMe", RpcTarget.All);
    }

    [PunRPC]
    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
