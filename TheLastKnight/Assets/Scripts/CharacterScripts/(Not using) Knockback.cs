using System.Collections;
using UnityEngine;

// Not used, but left here for future
public class Knockback : MonoBehaviour {

    public float thrust;
    public float knockTime;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Enemy")) 
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if (enemy != null) 
            {
                enemy.isKinematic = false;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(enemy));
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D enemy) 
    {
        if (enemy != null) 
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;
        }
    }
}