using System;
using System.Collections;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    [Header("---------Enemy Health----------")]
    public FloatValue maxHealth;

    [Header("----------Enemy movement---------")]
    public float moveSpeed = 2f;
    public float collisionOffSet = 0.04f;

    [Header("---------Attack---------")]
    public float attackDelay;
    
    [Header("-------WhiteFlash on hit---------")]
    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] private Material flashMaterial;

    [Tooltip("Duration of the flash.")]
    [SerializeField] private float flashDuration;

    private Material originalMaterial;
    private Coroutine flashRoutine;
    private Coroutine invulnerableRoutine;

    [Header("------Attack Sound-------")]
    [SerializeField] private AudioClip attackSoundClip;

    // protected
    protected bool canMove = true;
    protected Animator animator; 
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected GameObject player;
    protected float lastAttackTime = 0;
    protected bool alive = true;
    protected float health; 
    
    private void Start() {
        animator = GetComponent<Animator>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth.initialValue;
        player = GameObject.FindWithTag("Player");

        originalMaterial = spriteRenderer.material;
    }

    public abstract void TookDamage(float damage);

    public float getMaxHealth()
    {
        return maxHealth.initialValue;
    }
    
    public void Defeated() 
    {
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

    public void playAttackClip()
    {
        SoundFXManager.instance.PlaySoundFXClip(attackSoundClip, transform);
    }

    #region STAGGER AND WHITEFLASH
    public void Stagger() 
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
        animator.SetTrigger("Stagger"); 
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

}