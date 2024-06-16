using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator; 

    private void Start() {
        animator = GetComponent<Animator>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            
            // Player disppears
            collision.gameObject.SetActive(false);

            // go next level -> Scene changes once Disappear animation finishes
            Disappear();
        }
    }

    public void TriggerNextLevel() 
    {
        SceneController.instance.NextLevel();
    }

    private void Appear()
    {
        animator.Play("Portal_appear");
    }

    private void Disappear()
    {
        animator.Play("Portal_disappear");
    }

    private void Destroy()
    {
        Destroy(gameObject); 
    }
    
}
