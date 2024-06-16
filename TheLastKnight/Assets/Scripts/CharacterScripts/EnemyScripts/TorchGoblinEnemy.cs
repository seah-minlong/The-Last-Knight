using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TorchGoblinEnemy : MobAIChase
{
    private void FixedUpdate() {
        AIChase(chaseRadius, attackRadius);
    }

    public override void TookDamage(float damage) {
        
        health -= damage;
        if(alive)
        {
            if (health <= 0)
            {
                LockMovement();
                Defeated();
                alive = false;
            } else 
            {
                Stagger();
            }
        } 
    }
}
