using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class TargetController : RealtimeComponent
{
    //public
    public virtual void TakeDamage()
    {
        Debug.Log("Take Damage "+ gameObject.name);
    }
}
