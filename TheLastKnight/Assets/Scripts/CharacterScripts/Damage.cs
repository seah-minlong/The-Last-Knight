using System.Collections;
using UnityEngine;

// Not used, but left here for future
public class Damage : MonoBehaviour {

    public float damage = 3;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy") {
            //Deal damage to Enemy 
            Enemy enemy = other.GetComponent<Enemy>(); 

            if (enemy != null) {
                print("took damage"); 
                enemy.Health -= damage; 
            }
        }
    }
}