using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffSet = 0.05f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {

        if (canMove) {
            // If movement is not 0, try to move
            if (movementInput != Vector2.zero) {
                bool success = TryMove(movementInput);

                // Checks for "sliding" across objects
                if (!success) {
                    // If unable to move in x & y direction, try x-direction
                    success = TryMove(new Vector2(movementInput.x, 0));

                    if (!success) {
                        // Try y-direction
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }

                // TODO: move y direction -> walk up or down

                animator.SetBool("isMoving", success);
            } else {
                animator.SetBool("isMoving", false);
            }

            // Set direction of sprite to movement direction
            if (movementInput.x < 0) {
                spriteRenderer.flipX = true;
            } else if (movementInput.x > 0) {
                spriteRenderer.flipX = false;
            }
        }
    }

    private bool TryMove(Vector2 direction) {
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

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }
    
    void OnFire() {
        animator.SetTrigger("sideSlashSword");
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }
}