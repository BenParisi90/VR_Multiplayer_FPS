using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using Photon.Pun;

public class PlayerWeapon : MonoBehaviourPunCallbacks
{
    [SerializeField]
    List<GameObject> weapons;

    int currentWeapon = 0;


    bool waitingForWeaponSwitchRelease = false;

    override public void OnEnable()
    {
        base.OnEnable();
        SetWeapon(0);
    }

    [PunRPC]
    void SetWeapon(int weaponIndex)
    {
        currentWeapon = weaponIndex;
        for(int i = 0; i < weapons.Count; i ++)
        {
            weapons[i].SetActive(i == weaponIndex);
        }
    }

    void Update()
    {
        if(!photonView.IsMine)
        {
            return;
        }
        
        if(!waitingForWeaponSwitchRelease && OVRInput.Get(OVRInput.Button.One))
        {
            photonView.RPC("SetWeapon", RpcTarget.All, (currentWeapon == 0 ? 1 : 0));
            //currentWeaponIndex == 0 ? 1 : 0
            waitingForWeaponSwitchRelease = true;
        }
        else if(waitingForWeaponSwitchRelease && !OVRInput.Get(OVRInput.Button.One))
        {
            waitingForWeaponSwitchRelease = false;
        }
    }
}
