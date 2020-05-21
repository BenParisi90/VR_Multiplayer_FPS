using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    bool triggerReleased = true;
    public Transform shootPoint;
    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > .5 && triggerReleased)
        {
            RaycastHit hit;
            if(Physics.Raycast(shootPoint.position, shootPoint.forward, out hit))
            {
                Debug.Log("HIT " + hit.transform.name);
                if(hit.transform.tag == "Target")
                {
                    hit.transform.GetComponent<TargetController>().TakeDamage();
                }
            }
            triggerReleased = false;
        }
        else if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) < .5 && !triggerReleased)
        {
            triggerReleased = true;
        }
    }
}
