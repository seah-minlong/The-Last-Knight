using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    // public
    public float moveSpeed = 1f;
    public float collisionOffSet = 0.05f;
    public FloatValue currentHealth;
    
    // private
    private ContactFilter2D movementFilter; 
    private Vector2 movementInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Vector3 change;
    private bool canMove = true;

    // attacks
    public SideSlash sideSlash;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        if (canMove) {
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
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
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            return true;
        } else {
            return false;
        }

    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }
    
    void OnFire() {
        animator.SetTrigger("attack");
    }

    public void SideSlash() {
        LockMovement();
        if(spriteRenderer.flipX == true) {
            sideSlash.AttackLeft(); 
        } else {
            sideSlash.AttackRight(); 
        }
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }
}