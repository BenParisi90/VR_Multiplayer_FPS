using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class GunController : RealtimeComponent
{
    bool triggerReleased = true;
    public Transform shootPoint;
    public ParticleSystem muzzleFlashPrefab;
    public RealtimeTransform avatarRealtimeTransform;
    // Update is called once per frame
    void Update()
    {
        if(avatarRealtimeTransform.isOwnedLocally && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > .5 && triggerReleased)
        {
            //add a muzzle flash for all players
            Realtime.Instantiate(muzzleFlashPrefab.name, shootPoint.position, shootPoint.rotation);
            //if the bullet hit something
            RaycastHit hit;
            if(Physics.Raycast(shootPoint.position, shootPoint.forward, out hit))
            {
                Debug.Log("HIT " + hit.transform.name);
                if(hit.transform.tag == "Target")
                {
                    hit.transform.GetComponent<TargetController>().TakeDamage();
                }
            }
            //record that the trigger is held down
            triggerReleased = false;
        }
        else if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) < .5 && !triggerReleased)
        {
            triggerReleased = true;
        }
    }
}
