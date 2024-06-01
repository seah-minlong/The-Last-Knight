using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownSlash : MonoBehaviour
{
    public float damage = 3;

    private void OnTriggerEnter2D(Collider2D other) {
        print("enter"); 
        if(other.tag == "Enemy") {
            //Deal damage to Enemy 
            TorchGoblinEnemy torchGoblinEnemy = other.GetComponent<TorchGoblinEnemy>(); 

            if (torchGoblinEnemy != null) {
                print("took damage"); 
                torchGoblinEnemy.Health -= damage; 
            }
        }
    }
}
