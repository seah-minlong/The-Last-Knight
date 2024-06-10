using UnityEngine;

[ExecuteInEditMode] // Allows the script to run in the editor without entering Play mode
public class DrawBoxColliders : MonoBehaviour
{
    void OnDrawGizmos()
    {
        // Get all BoxCollider2D components attached to this GameObject and its children
        BoxCollider2D[] colliders = GetComponentsInChildren<BoxCollider2D>();

        // Set the color for the Gizmos
        Gizmos.color = Color.red;

        // Iterate through each BoxCollider2D and draw its bounds
        foreach (BoxCollider2D collider in colliders)
        {
            // Get the collider's position and size
            Vector3 colliderPosition = collider.transform.position + (Vector3)collider.offset;
            Vector3 colliderSize = new Vector3(collider.size.x, collider.size.y, 1);

            // Draw a wireframe cube around the collider
            Gizmos.DrawWireCube(colliderPosition, colliderSize);
        }
    }
}
