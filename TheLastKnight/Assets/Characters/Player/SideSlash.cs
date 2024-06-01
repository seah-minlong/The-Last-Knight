using UnityEngine;

public class SideSlash : MonoBehaviour
{
    public float damage = 3;
    private BoxCollider2D swordCollider; 
    private Vector2 rightAttackOffset; 

    // Start is called before the first frame update
    void Start()
    {
        swordCollider = GetComponent<BoxCollider2D>();
        rightAttackOffset = transform.position; 
    }

    public void AttackRight() {
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft() { 
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y); // flip the hitbox when attacking left
    }

    private void OnTriggerEnter2D(Collider2D other) {
        print("enter"); 
        if(other.tag == "Enemy") {
            //Deal damage to Enemy 
            TorchGoblinEnemy torchGoblinEnemy = other.GetComponent<TorchGoblinEnemy>(); 

            if (torchGoblinEnemy != null) {
                print("took damage"); 
                torchGoblinEnemy.Health -= damage; 
            }
        }
    }
}
