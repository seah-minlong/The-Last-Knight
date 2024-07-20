using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] GameObject currCheckpoint; 
    [SerializeField] private AudioClip checkpointSoundClip;
    protected Animator animator; 
    PlayerController playerController; 
    private float lastCheckpointTime = -60f;

    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    public void Awake() {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); 
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("triggered"); 
        if (collision.CompareTag("Player") && Time.time >= lastCheckpointTime + 60f) {
            Debug.Log("collided with cp"); 
            animator.SetTrigger("checkpoint"); 
            SoundFXManager.instance.PlaySoundFXClip(checkpointSoundClip, transform);
            playerController.UpdateCheckpoint(currCheckpoint.transform.position); 
            lastCheckpointTime = Time.time;
        }
    }
}
