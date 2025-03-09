using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    [SerializeField] private float minDistance = 1.5f; // Distance from spheres to restrict movement

    void Update()
    {
        // Get input from WASD or Arrow Keys
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movementDirection = new Vector3(horizontal, 0, vertical).normalized;

        // If there is movement input
        if (movementDirection != Vector3.zero)
        {
            // Check if movement is allowed (not blocked by a sphere)
            if (CanMove(movementDirection))
            {
                transform.position += movementDirection * moveSpeed * Time.deltaTime;
            }
        }
    }

    private bool CanMove(Vector3 direction)
    {
        RaycastHit hit;
        float checkDistance = minDistance + 0.1f; // Buffer to prevent clipping

        // Cast a ray from the camera in the movement direction
        if (Physics.Raycast(transform.position, direction, out hit, checkDistance))
        {
            if (hit.collider.CompareTag("Sphere")) // If we hit a sphere
            {
                return false; // Block movement
            }
        }

        return true; 
    }
}
