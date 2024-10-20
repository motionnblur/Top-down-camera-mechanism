using System;
using UnityEngine;

enum CameraMode { IDLE, MOVING_TO_PLAYER, IN_PLAYER_ORBIT }
public class LookAt : MonoBehaviour
{

    public static LookAt Instance { get; private set; }
    CameraMode currentMode = CameraMode.IDLE;
    public Transform player; // Assign the player GameObject in the Inspector
    public float orbitSpeed = 5.0f; // Speed of camera orbit
    public float orbitRadius = 5.0f; // Distance between camera and player
    private float horizontalAngle = -1.5f; // Initial horizontal angle of camera orbit
    private float verticalAngle = 0.6f; // Initial vertical angle of camera orbit

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void LateUpdate()
    {
        switch (currentMode)
        {
            case CameraMode.IDLE:
                Idle();
                break;
            case CameraMode.MOVING_TO_PLAYER:
                MovingToPlayer();
                break;
            case CameraMode.IN_PLAYER_ORBIT:
                InPlayerOrbit();
                break;
        }
    }

    void Idle()
    {
        if (SelectManager.Instance.currentSelectedPlayer != null)
        {
            player = SelectManager.Instance.currentSelectedPlayer.transform;
            currentMode = CameraMode.MOVING_TO_PLAYER;
        }
    }
    void MovingToPlayer()
    {
        float x = orbitRadius * Mathf.Cos(horizontalAngle) * Mathf.Cos(verticalAngle);
        float y = orbitRadius * Mathf.Sin(verticalAngle);
        float z = orbitRadius * Mathf.Sin(horizontalAngle) * Mathf.Cos(verticalAngle);

        Vector3 cameraPosition = player.position + new Vector3(x, y, z);
        transform.position = Vector3.Lerp(transform.position, cameraPosition, Time.deltaTime * 2f);

        transform.LookAt(player);

        if (Vector3.Distance(transform.position, cameraPosition) < 0.1f)
            currentMode = CameraMode.IN_PLAYER_ORBIT;
    }
    void InPlayerOrbit()
    {
        if (Input.GetMouseButton(1)) // 1 is the index for the right mouse button
        {
            if (Input.mousePositionDelta.x != 0 || Input.mousePositionDelta.y != 0)
            {
                float x = orbitRadius * Mathf.Cos(horizontalAngle) * Mathf.Cos(verticalAngle);
                float y = orbitRadius * Mathf.Sin(verticalAngle);
                float z = orbitRadius * Mathf.Sin(horizontalAngle) * Mathf.Cos(verticalAngle);

                Vector3 cameraPosition = player.position + new Vector3(x, y, z);
                transform.position = cameraPosition;

                transform.LookAt(player);

                horizontalAngle += Input.mousePositionDelta.x * orbitSpeed * Time.deltaTime;
                verticalAngle -= Input.mousePositionDelta.y * orbitSpeed * Time.deltaTime;

#if UNITY_EDITOR
                Debug.Log($"Horizontal Angle: {horizontalAngle}, Vertical Angle: {verticalAngle}");
#endif

                verticalAngle = Mathf.Clamp(verticalAngle, 0f, 1.2f);
            }

        }
    }
    public void ChangePlayer(Transform newPlayer)
    {
        player = newPlayer;
        currentMode = CameraMode.MOVING_TO_PLAYER;
    }
}