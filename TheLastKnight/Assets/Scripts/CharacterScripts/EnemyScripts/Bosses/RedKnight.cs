
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKnight : Bosses
{ 
    [Header("--------Boss Properties------")]
    [SerializeField] float chaseRadius = 4;
    [SerializeField] float attackRadius = 2;
    

    [Header("--------Attacks------")]
    [SerializeField] SideSlash sideSlash;
    [SerializeField] private Enemy bomb;
    
    private List<Vector3> bombPositionList;
    private ContactFilter2D movementFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private bool firstContact = true;

    private new void Awake() {
        base.Awake();
        bombPositionList = new List<Vector3>();

        foreach (Transform bombPosition in transform.Find("bombPositions"))
        {
            bombPositionList.Add(bombPosition.position);
        }
    }
    private void FixedUpdate() {
        AIChase(chaseRadius, attackRadius);
    }
    
    #region MOVEMENT AND ATTACK
    protected void AIChase(float chaseRadius, float attackRadius) {
        
        if (canMove) {
            
            // AI Chase
            float distance = Vector3.Distance(transform.position, player.transform.position);
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // Set direction of sprite to movement direction
            if (direction.x < 0) 
            {
                spriteRenderer.flipX = true;
            } 
            else if (direction.x > 0) 
            {
                spriteRenderer.flipX = false;
            }

            if (distance <= attackRadius)
            {
                animator.SetBool("isMoving", false);
                // Check to see if enough time has passed since we last attacked
                // If first contact, just attack
                if (firstContact || Time.time > lastAttackTime + attackDelay) 
                {
                    direction.x = Mathf.Round(direction.x);
                    direction.y = Mathf.Round(direction.y);
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
    #endregion

    public override void TookDamage(float damage) {
        
        health -= damage;

        // play sound FX 
        SoundFXManager.instance.PlaySoundFXClip(damageSoundClip, transform);

        if(alive)
        {
            if (health <= 0)
            {
                // Stop Music
                SoundMenuManager.instance.PauseMusic();

                LockMovement();
                Defeated();
                alive = false;
            } 
            else
            {
                Stagger();
                SpawnBomb();   
                if (!Stage2 && health <= halfHealth)
                {
                    Debug.Log("Stage 2");
                    Stage2 = true;

                    // Play Stage 2 Music
                    BossMusic.instance.Stage2Music();

                    SpawnEnemy(); 
                }
            }
        } 
    }

    private void SpawnBomb()
    {
        int rand = Random.Range(0, bombPositionList.Count);
        Vector3 bombPosition = bombPositionList[rand];
        Instantiate(bomb, bombPosition, Quaternion.identity);
    }
}
