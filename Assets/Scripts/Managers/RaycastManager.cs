using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null)
            {
                EventManager.TriggerEvent("OnRaycastHit", hit.collider);
            }
        }
    }
}
