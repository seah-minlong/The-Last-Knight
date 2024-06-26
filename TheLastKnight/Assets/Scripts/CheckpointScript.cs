using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    PlayerController playerController; 
    [SerializeField] GameObject currCheckpoint; 

    public void Awake() {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); 
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("triggered"); 
        if (collision.CompareTag("Player")) {
            Debug.Log("collided with cp"); 
            playerController.UpdateCheckpoint(currCheckpoint.transform.position); 
        }
    }
}
