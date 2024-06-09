using System.Collections;
using UnityEngine;

public class Damage : MonoBehaviour {

    public float damage = 1;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy")) {
            Debug.Log("contacted enemy");
            //Deal damage to Enemy 
            Enemy enemy = other.GetComponent<Enemy>(); 

            if (enemy != null) 
            {
                Debug.Log("took damage"); 
                enemy.Health -= damage; 
            }
        } 
        
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player hit");
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null) 
            {
                Debug.Log("took damage");
                player.TookDamage(damage);
            }
        } 
    }
}