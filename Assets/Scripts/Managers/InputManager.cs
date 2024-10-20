using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventManager.TriggerEvent("OnLeftClick", true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EventManager.TriggerEvent("OnLeftClick", false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            EventManager.TriggerEvent("OnRightClick", true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            EventManager.TriggerEvent("OnRightClick", false);
        }
    }
}
