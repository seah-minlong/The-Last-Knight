using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour {

    [SerializeField] float damage = 1;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(gameObject.CompareTag("Player") && other.gameObject.CompareTag("Enemy")) {
            Debug.Log("player hit enemy");
            //Deal damage to Enemy 
            Enemy enemy = other.GetComponent<Enemy>(); 

            if (enemy != null) 
            {
                Debug.Log("took damage"); 
                enemy.TookDamage(damage); 
            }
        } 
        
        if (gameObject.CompareTag("Enemy") && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("enemy hit player");
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null) 
            {
                Debug.Log("took damage");
                player.TookDamage(damage);
            }
        } 

        if (other.gameObject.CompareTag("Env obj"))
        {
            Debug.Log("hit env");
            EnvObj entity = other.GetComponent<EnvObj>();

            if (entity != null) 
            {
                Debug.Log("entity took dmg");
                entity.TookDamage(damage);
            }
        }
    }
}