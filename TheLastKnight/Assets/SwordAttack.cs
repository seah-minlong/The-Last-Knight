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
        swordCollider = GetComponent<Collider2D>();
        rightAttackOffset = transform.position; 
    }

    public void AttackRight() {
        transform.localPosition = rightAttackOffset; 
    }

    public void AttackLeft() { 
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
