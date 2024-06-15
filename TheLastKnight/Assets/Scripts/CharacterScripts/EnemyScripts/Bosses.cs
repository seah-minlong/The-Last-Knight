using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosses : MobAIChase
{ 
    public GameObject portal; 
    private int counter = 0;
    private void FixedUpdate() {
        AIChase(chaseRadius, attackRadius);
    }

    public override float Health {
        set {
            print(value);
            health = value;
            
            if(alive && health <= 0) 
            {
                LockMovement();
                Defeated();
                alive = false;
                Instantiate(portal);
            }
            else if (alive && counter % 2 == 0)
            {
                // Stagger every 2 hits
                Stagger();
            }
            counter++;
        }
        get {
            return health;
        }
    }

    public void Stagger()
    {
        animator.SetTrigger("Stagger"); 
    }
}
