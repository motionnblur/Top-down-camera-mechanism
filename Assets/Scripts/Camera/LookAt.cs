using UnityEngine;

public class CameraOrbitPlayer : MonoBehaviour
{
    public Transform player; // Assign the player GameObject in the Inspector
    public float orbitSpeed = 5.0f; // Speed of camera orbit
    public float orbitRadius = 5.0f; // Distance between camera and player

    private float horizontalAngle = 0.0f; // Initial horizontal angle of camera orbit
    private float verticalAngle = 0.0f; // Initial vertical angle of camera orbit

    void LateUpdate()
    {
        // Calculate camera position based on orbit radius and angles
        float x = orbitRadius * Mathf.Cos(horizontalAngle) * Mathf.Cos(verticalAngle);
        float y = orbitRadius * Mathf.Sin(verticalAngle);
        float z = orbitRadius * Mathf.Sin(horizontalAngle) * Mathf.Cos(verticalAngle);
        transform.position = player.position + new Vector3(x, y, z);

        // Make camera look at player
        transform.LookAt(player);

        // Update angles based on mouse input
        horizontalAngle += Input.GetAxis("Mouse X") * orbitSpeed * Time.deltaTime;
        verticalAngle += Input.GetAxis("Mouse Y") * orbitSpeed * Time.deltaTime;

        // Clamp vertical angle to prevent camera from flipping over
        verticalAngle = Mathf.Clamp(verticalAngle, -90f, 90f);
    }
}