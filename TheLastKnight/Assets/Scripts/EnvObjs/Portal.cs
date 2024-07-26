using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private AudioClip enterPortalClip;
    [SerializeField] private AudioClip portalSpawnClip;
    
    private Animator animator; 

    private void Start() {
        // play sound FX 
        SoundFXManager.instance.PlaySoundFXClip(portalSpawnClip, transform);

        animator = GetComponent<Animator>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            // play sound FX 
            SoundFXManager.instance.PlaySoundFXClip(enterPortalClip, transform);
            
            // Player disppears
            collision.gameObject.SetActive(false);

            // go next level -> Scene changes once Disappear animation finishes
            Disappear();
            Debug.Log("next level and respawn count resetted"); 
            PlayerController.SetIsNextLevel(true);
            PlayerPrefs.SetInt("RespawnCount", 0);
            Debug.Log("player game level is : " + PlayerController.isNextLevel);  
        }
    }

    public void TriggerNextLevel() 
    {
        SceneController.instance.NextLevel();
    }

    public void TriggerVictoryScreen()
    {
        VictoryMenuScript.instance.Victory();
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
