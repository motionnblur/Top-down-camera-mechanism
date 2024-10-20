using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform player; // Assign the player GameObject in the Inspector
    public float orbitSpeed = 5.0f; // Speed of camera orbit
    public float orbitRadius = 5.0f; // Distance between camera and player

    private bool onRightClick = false;
    private float horizontalAngle = 0.0f; // Initial horizontal angle of camera orbit
    private float verticalAngle = 0.0f; // Initial vertical angle of camera orbit

    void OnEnable()
    {
        EventManager.AddEvent<bool>("OnRightClick", OnRightClick);
    }
    void OnDisable()
    {
        EventManager.RemoveEvent<bool>("OnRightClick", OnRightClick);
    }

    void LateUpdate()
    {
        if (!onRightClick) return;

        float x = orbitRadius * Mathf.Cos(horizontalAngle) * Mathf.Cos(verticalAngle);
        float y = orbitRadius * Mathf.Sin(verticalAngle);
        float z = orbitRadius * Mathf.Sin(horizontalAngle) * Mathf.Cos(verticalAngle);

        Vector3 cameraPosition = player.position + new Vector3(x, y, z);
        transform.position = cameraPosition;

        transform.LookAt(player);

        horizontalAngle += Input.GetAxis("Mouse X") * orbitSpeed * Time.deltaTime;
        verticalAngle -= Input.GetAxis("Mouse Y") * orbitSpeed * Time.deltaTime;

        verticalAngle = Mathf.Clamp(verticalAngle, 1.8f, 3.15f);
    }

    void OnRightClick(bool stage)
    {
        onRightClick = stage;
    }
}