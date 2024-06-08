using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    // public variables
    public FloatValue maxHealth;
    public float health; 
    public float moveSpeed = 2f;
    public float collisionOffSet = 0.04f;
    public float attackDmg = 1;
    
    // protected
    protected bool alive = true;
    protected bool canMove = true;
    protected Animator animator; 
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;

    protected GameObject player;

    private void Start() {
        animator = GetComponent<Animator>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth.initialValue;
        player = GameObject.FindWithTag("Player");
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
    
    public abstract void Defeated();

    public void RemoveEnemy() {
        Destroy(gameObject); 
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }
}