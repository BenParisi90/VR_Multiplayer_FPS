using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using Photon.Pun;

public class PlayerWeapon : MonoBehaviourPunCallbacks
{
    [SerializeField]
    List<GameObject> weapons;

    [SerializeField]
    Transform shootTransform;

    int currentWeapon = 0;


    bool waitingForWeaponShootRelease = false;
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

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(shootTransform.position, shootTransform.forward, out hit))
        {
            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage();
            }
        }
    }

    void Update()
    {
        if(!photonView.IsMine)
        {
            return;
        }

        //switching weapons
        if(!waitingForWeaponSwitchRelease && OVRInput.Get(OVRInput.Button.One))
        {
            photonView.RPC("SetWeapon", RpcTarget.All, (currentWeapon == 0 ? 1 : 0));
            waitingForWeaponSwitchRelease = true;
        }
        else if(waitingForWeaponSwitchRelease && !OVRInput.Get(OVRInput.Button.One))
        {
            waitingForWeaponSwitchRelease = false;
        }

        //shooting
        if(!waitingForWeaponShootRelease && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > .5)
        {
            Shoot();
            waitingForWeaponShootRelease = true;
        }
        else if(waitingForWeaponShootRelease && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) < .5)
        {
            waitingForWeaponShootRelease = false;
        }
    }
}
