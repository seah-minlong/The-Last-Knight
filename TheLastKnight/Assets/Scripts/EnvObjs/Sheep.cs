using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : EnvObj
{
    public override void TookDamage(float damage)
    {
        health -= damage;
        if(alive)
        {
            if (health <= 0)
            {
                Death();
                alive = false;
            } else 
            {
                Hit();
            }
        } 
    }
}
