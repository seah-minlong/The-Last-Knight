using UnityEngine;

[ExecuteInEditMode] // Allows the script to run in the editor without entering Play mode
public class DrawAllColliders : MonoBehaviour
{
    void OnDrawGizmos()
    {
        // Draw BoxColliders
        BoxCollider2D[] boxColliders = GetComponentsInChildren<BoxCollider2D>();
        Gizmos.color = Color.red;
        foreach (BoxCollider2D collider in boxColliders)
        {
            Vector3 colliderPosition = collider.transform.position + (Vector3)collider.offset;
            Vector3 colliderSize = new Vector3(collider.size.x, collider.size.y, 1);
            Gizmos.DrawWireCube(colliderPosition, colliderSize);
        }

        // Draw CircleColliders
        CircleCollider2D[] circleColliders = GetComponentsInChildren<CircleCollider2D>();
        Gizmos.color = Color.green;
        foreach (CircleCollider2D collider in circleColliders)
        {
            Vector3 colliderPosition = collider.transform.position + (Vector3)collider.offset;
            Gizmos.DrawWireSphere(colliderPosition, collider.radius);
        }

        // Draw CapsuleColliders
        CapsuleCollider2D[] capsuleColliders = GetComponentsInChildren<CapsuleCollider2D>();
        Gizmos.color = Color.blue;
        foreach (CapsuleCollider2D collider in capsuleColliders)
        {
            Vector3 colliderPosition = collider.transform.position + (Vector3)collider.offset;
            Vector3 colliderSize = new Vector3(collider.size.x, collider.size.y, 1);
            Gizmos.DrawWireCube(colliderPosition, colliderSize); // Approximation
        }

        // Draw PolygonColliders
        PolygonCollider2D[] polygonColliders = GetComponentsInChildren<PolygonCollider2D>();
        Gizmos.color = Color.yellow;
        foreach (PolygonCollider2D collider in polygonColliders)
        {
            Vector3 colliderPosition = collider.transform.position;
            for (int i = 0; i < collider.pathCount; i++)
            {
                Vector2[] path = collider.GetPath(i);
                for (int j = 0; j < path.Length; j++)
                {
                    Vector2 startPoint = path[j];
                    Vector2 endPoint = path[(j + 1) % path.Length];
                    Gizmos.DrawLine(colliderPosition + (Vector3)startPoint, colliderPosition + (Vector3)endPoint);
                }
            }
        }
    }
}
