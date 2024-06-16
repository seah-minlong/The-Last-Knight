using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

abstract public class MobAIChase : Enemy
{
    public float chaseRadius = 4;
    public float attackRadius = 2;

    // attacks
    public SideSlash sideSlash;

    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private bool firstContact = true;
    
    // Input distance to start chasing player
    protected void AIChase(float chaseRadius, float attackRadius) {
        
        if (canMove) {
            
            // AI Chase
            float distance = Vector3.Distance(transform.position, player.transform.position);
            Vector3 direction = (player.transform.position - transform.position).normalized;

            if (distance <= attackRadius)
            {
                
                animator.SetBool("isMoving", false);
                // Check to see if enough time has passed since we last attacked
                // If first contact, just attack
                if (firstContact || Time.time > lastAttackTime + attackDelay) 
                {
                    animator.SetFloat("moveX", direction.x);
                    animator.SetFloat("moveY", direction.y);
                    animator.SetTrigger("attack");
                    
                    // Record the time we attacked
                    lastAttackTime = Time.time;
                    firstContact = false;
                }
            } else if (distance <= chaseRadius && distance > attackRadius) 
            {
                bool success = TryMove(direction);

                // Checks for "sliding" across objects
                if (!success) 
                {
                    // If unable to move in x & y direction, try x-direction
                    success = TryMove(new Vector2(direction.x, 0));

                    if (!success) 
                    {
                        // Try y-direction
                        success = TryMove(new Vector2(0, direction.y));
                    }
                }
                // animator.SetFloat("moveX", direction.x);
                // animator.SetFloat("moveY", direction.y);
                animator.SetBool("isMoving", success);

                // Set direction of sprite to movement direction
                if (direction.x < 0) 
                {
                    spriteRenderer.flipX = true;
                } 
                else if (direction.x > 0) 
                {
                    spriteRenderer.flipX = false;
                }
            } 
            else 
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    public bool TryMove(Vector2 direction) 
    {
        if (direction == Vector2.zero) 
        {
            return false;
        }
        // Check for potential collisions
        int count = rb.Cast(
            direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
            castCollisions, // List of coliisions to store the found collisions after the Cast is finished
            moveSpeed * Time.fixedDeltaTime + collisionOffSet); // The amount to cast equal to the movement plus an offset
        
        
        if (count == 0) 
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        } 
        else 
        {
            return false;
        }

    }

    public void SideSlash() 
    {
        LockMovement();
        if (spriteRenderer.flipX == true) 
        {
            sideSlash.AttackLeft(); 
        } 
        else 
        {
            sideSlash.AttackRight(); 
        }
    }
}
