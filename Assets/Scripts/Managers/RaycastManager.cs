using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    private bool onLeftClick = false;
    void OnEnable()
    {
        EventManager.AddEvent<bool>("OnLeftClick", OnLeftClick);
    }
    void OnDisable()
    {
        EventManager.RemoveEvent<bool>("OnLeftClick", OnLeftClick);
    }
    void Update()
    {
        if (!onLeftClick) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null)
            {
                EventManager.TriggerEvent("OnRaycastHit", hit.collider);
            }
        }
    }
    void OnLeftClick(bool stage)
    {
        onLeftClick = stage;
    }
}
