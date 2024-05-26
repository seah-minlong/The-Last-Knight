using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    Collider2D swordCollider; 
    Vector2 rightAttackOffset; 

    // Start is called before the first frame update
    void Start() {
        swordCollider = GetComponent<Collider2D>(); 
        rightAttackOffset = transform.position; 
        
    }

    public void AttackRight() {
        //print("right"); 
        swordCollider.enabled = true; 
        transform.position = rightAttackOffset; 

    }

    public void AttackLeft() {
        //print("left"); 
        swordCollider.enabled = true; 
        transform.position = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y); //flip the hitbox when attacking left 

    }

    public void StopAttack() {
        swordCollider.enabled = false; 

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy") {
            
        }
    }

    
}
