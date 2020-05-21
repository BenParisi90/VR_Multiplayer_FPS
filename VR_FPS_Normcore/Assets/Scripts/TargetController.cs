using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class TargetController : RealtimeComponent
{
    //public
    public void TakeDamage()
    {
        Debug.Log("Take Damage");
        GameController.instance.ChangeScore(1);
    }
}
