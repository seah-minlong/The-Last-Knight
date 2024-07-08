using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnvObj : MonoBehaviour
{
    public float health = 2;
    protected bool alive = true;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    private void Start() 
    {
        animator = GetComponent<Animator>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void RemoveEnemy() 
    {
        Destroy(gameObject); 
    }

    public void Death()
    {
        animator.SetTrigger("Dead");
    }

    public void Hit()
    {
        animator.SetTrigger("Hit");
    }

    public abstract void TookDamage(float damage);
}
