using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTree : EnvObj
{
    public override void TookDamage(float damage)
    {
        Hit();
    }
}
