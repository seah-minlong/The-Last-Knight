using System;
using Unity.VisualScripting;
using UnityEngine;

public class TorchGoblinEnemy : MonoBehaviour
{
    Animator animator; 
    public float health = 1; 
    SpriteRenderer spriteRenderer;
    public GameObject player;
    private float distance;
    public float moveSpeed = 1f;
    public float collisionOffSet = 0.05f;
    public ContactFilter2D movementFilter;
    bool canMove = true;

    private void Start() {
        animator = GetComponent<Animator>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public float Health {
        set {
            print(value); 
            health = value;

            if(health <= 0) {
                LockMovement();
                Defeated();
            }
        }
        get {
            return health;
        }
    }
    
    public void Defeated() {
        animator.SetTrigger("Defeated"); 
    }

    public void RemoveEnemy() {
        Destroy(gameObject); 
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }

    // Update is called once per frame
    private void Update() 
    {
        if (canMove) {
            // AI Chase
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;

            // If player not at current position, try to move
            if (distance > 0) {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                animator.SetBool("isMoving", true);
            } else {
                animator.SetBool("isMoving", false);
            } 

            // Set direction of sprite to movement direction
            if (direction.x < 0) {
                spriteRenderer.flipX = true;
            } else if (direction.x > 0) {
                spriteRenderer.flipX = false;
            }
        }
        
    }
}
