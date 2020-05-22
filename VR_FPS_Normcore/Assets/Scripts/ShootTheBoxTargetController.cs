using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTheBoxTargetController : TargetController
{
    override public void TakeDamage()
    {
        base.TakeDamage();
        GameController.instance.ChangeScore(1);
    }
}
