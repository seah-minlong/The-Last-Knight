using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor; 


public class PlayerController : MonoBehaviour
{
    [Header("-------Player movement-------")]
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float collisionOffSet = 0.05f;
    [SerializeField] SideSlash sideSlash;


    [Header("---------Player health----------")]
    [SerializeField] FloatValue currentHealth;
    [SerializeField] MySignal playerHealthSignal;
    

    [Header("--------WhiteFlash on hit------")]
    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] private Material flashMaterial;

    [Tooltip("--------Duration of the flash-----")]
    [SerializeField] private float flashDuration;


    [Header("-------Audio-------")]
    [SerializeField] private AudioClip damageSoundClip; 
    [SerializeField] private AudioClip swordClip1; 
    [SerializeField] private AudioClip swordClip2; 
    [SerializeField] private AudioClip walkingClip1; 
    [SerializeField] private AudioClip walkingClip2; 
    [SerializeField] private float walkingVolume;


    [Header("-------Game Over-------")]
    [SerializeField] private GameOverScript gameOverScreen; 

    [Header("-------Spawn/Respawn-------")]
    [SerializeField] GameObject player; 
    [SerializeField] GameObject spawnPoint; 

    private Material originalMaterial;
    private Coroutine flashRoutine;

    // private
    private ContactFilter2D movementFilter; 
    private Vector2 movementInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Vector3 change;
    private bool canMove = true;
    private bool alive = true;
    private static Vector2 checkpointPos = Vector2.zero; 
    private static int respawnCount = 0; 
    public static bool isNextLevel = false;  

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); 
        spawnPoint = GameObject.FindWithTag("SpawnPoint"); 
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        
        respawnCount = PlayerPrefs.GetInt("RespawnCount", 0); 
        Debug.Log("start in player controller called " + isNextLevel + respawnCount);

        if(respawnCount == 0) {
            Debug.Log("if for respawn count called"); 
            player.transform.position = spawnPoint.transform.position; 
            checkpointPos = spawnPoint.transform.position; 
        } else {     
            Debug.Log("else for respawn count called");    
            player.transform.position = checkpointPos; 
        }
        Debug.Log("player respawn count" + respawnCount);
    }

    #region MOVEMENT
    private void FixedUpdate() {
        if (canMove) 
        {
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            // If movement is not 0, try to move
            if (movementInput != Vector2.zero) 
            {
                bool success = TryMove(movementInput);

                // Checks for "sliding" across objects
                if (!success) 
                {
                    // If unable to move in x & y direction, try x-direction
                    success = TryMove(new Vector2(movementInput.x, 0));

                    if (!success) 
                    {
                        // Try y-direction
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }
                animator.SetBool("isMoving", success);
            } 
            else 
            {
                animator.SetBool("isMoving", false);
            }

            // Set direction of sprite to movement direction
            if (movementInput.x < 0) 
            {
                spriteRenderer.flipX = true;
            } 
            else if (movementInput.x > 0) 
            {
                spriteRenderer.flipX = false;
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
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            return true;
        } 
        else {
            return false;
        }
    }
    
    public void LockMovement() 
    {
        canMove = false;
    }

    public void UnlockMovement() 
    {
        canMove = true;
    }

    #region SOUND
    public void playSwordClip1()
    {
        SoundFXManager.instance.PlaySoundFXClip(swordClip1, transform);
    }

    public void playSwordClip2()
    {
        SoundFXManager.instance.PlaySoundFXClip(swordClip2, transform);
    }

    public void playWalkingClip1()
    {
        SoundFXManager.instance.PlaySoundFXClipAtVol(walkingClip1, transform, walkingVolume);
    }

    public void playWalkingClip2()
    {
        SoundFXManager.instance.PlaySoundFXClipAtVol(walkingClip2, transform, walkingVolume);
    }

    public void playHurtClip()
    {
        SoundFXManager.instance.PlaySoundFXClip(damageSoundClip, transform);
    }
    #endregion

    void OnMove(InputValue movementValue) 
    {
        movementInput = movementValue.Get<Vector2>();
    }
    #endregion

    #region ATTACK
    void OnFire() 
    {
        if (!PauseMenuScript.isPaused && !VictoryMenuScript.instance.IsVictory())
        {
            animator.SetTrigger("attack");
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
    
    #region DAMAGE & HEALTH
    public void TookDamage(float damage) 
    {
        // play sound FX 
        SoundFXManager.instance.PlaySoundFXClip(damageSoundClip, transform);
        
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (alive & currentHealth.RuntimeValue <= 0) 
        {
            LockMovement();
            Defeated();
            alive = false;
        } else 
        {
            HitFlash();
        }
    }
    
    public void Defeated() 
    {
        animator.SetTrigger("Defeated");
        gameOverScreen.GameOver(); 
    }

    public void HitFlash() 
    {
        // If the flashRoutine is not null, then it is currently running.
        if (flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        // Swap to the flashMaterial.
        spriteRenderer.material = flashMaterial;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(flashDuration);

        // After the pause, swap back to the original material.
        spriteRenderer.material = originalMaterial;

        // Set the routine to null, signaling that it's finished.
        flashRoutine = null;
    }
    #endregion

    #region CHECKPOINT/RESPAWN
     public void UpdateCheckpoint(Vector2 pos) {
        Debug.Log("cp before" + checkpointPos); 
        checkpointPos = pos; 
        Debug.Log("cp after" + checkpointPos);
     }


     public Vector2 GetCheckpointPos() {
        return checkpointPos; 
     }

     public static int GetRespawnCount() 
     {
        return respawnCount; 
     }

     public static void ResetRespawnCount() 
     {
        respawnCount = 0; 
     }
    #endregion 

    # region IS NEXT LEVEL 

    public static bool GetIsNextLevel() 
    {
        return isNextLevel; 
    }

    public static void SetIsNextLevel(bool condition)
    {
        isNextLevel = condition; 
    }

    #endregion
    private void OnDisable()
    {
        Debug.Log("disable called"); 
        // Only reset respawn count when exiting Play mode in the Unity Editor
        #if UNITY_EDITOR
        if (!EditorApplication.isPlayingOrWillChangePlaymode && EditorApplication.isPlaying)
        {
            Debug.Log("inside disabled"); 
            PlayerPrefs.SetInt("RespawnCount", 0);
            PlayerPrefs.Save();
        }
        #endif
    }
}