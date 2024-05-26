using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 3;  
    public Collider2D swordCollider; 
    Vector2 rightAttackOffset; 

    // Start is called before the first frame update
    void Start() {
        rightAttackOffset = transform.position; 
    }

    public void AttackRight() {
        swordCollider.enabled = true; 
        transform.localPosition = rightAttackOffset; 

    }

    public void AttackLeft() { 
        swordCollider.enabled = true; 
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y); //flip the hitbox when attacking left 

    }

    public void StopAttack() {
        swordCollider.enabled = false; 

    }

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
