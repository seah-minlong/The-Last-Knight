using UnityEngine;

public class SideSlash : MonoBehaviour
{
    private Vector2 rightAttackOffset; 

    // Start is called before the first frame update
    void Start()
    {
        rightAttackOffset = transform.position; 
    }

    public void AttackRight() {
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft() { 
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y); // flip the hitbox when attacking left
    }
}
