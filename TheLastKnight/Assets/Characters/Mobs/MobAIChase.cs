using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

abstract public class MobAIChase : MonoBehaviour
{
    Animator animator; 
    public float health = 1; 
    private bool alive = true;
    SpriteRenderer spriteRenderer;
    public GameObject player;
    private float distance;
    public float moveSpeed = 1f;
    public float collisionOffSet = 0.05f;
    public ContactFilter2D movementFilter;
    bool canMove = true;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private void Start() {
        animator = GetComponent<Animator>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public float Health {
        set {
            print(value); 
            health = value;

            if(alive && health <= 0) {
                LockMovement();
                Defeated();
                alive = false;
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
    
    // Input distance to start chasing player
    protected void AIChase(float distanceFromPlayer) {
        if (canMove) {
            // AI Chase
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = (player.transform.position - transform.position).normalized;
            
            if (distance < distanceFromPlayer) {
                bool success = TryMove(direction);

                // Checks for "sliding" across objects
                if (!success) {
                    // If unable to move in x & y direction, try x-direction
                    success = TryMove(new Vector2(direction.x, 0));

                    if (!success) {
                        // Try y-direction
                        success = TryMove(new Vector2(0, direction.y));
                    }
                }
                animator.SetBool("isMoving", success);
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

    public bool TryMove(Vector2 direction) {
        if (direction == Vector2.zero) {
            return false;
        }
        // Check for potential collisions
        int count = rb.Cast(
            direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
            castCollisions, // List of coliisions to store the found collisions after the Cast is finished
            moveSpeed * Time.fixedDeltaTime + collisionOffSet); // The amount to cast equal to the movement plus an offset

        if (count == 0) {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        } else {
            return false;
        }

    }
}
