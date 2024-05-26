using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchGoblinEnemy : MonoBehaviour
{
    Animator animator; 
    public float Health {
        set {
            //print("value"); 
            health = value;

            if(health <= 0) {
                Defeated();
            }
        }
        get {
            return health;
        }
    }
    public float health = 1; 
    
    public void Defeated() {
        Destroy(gameObject); 
    }

    
}
