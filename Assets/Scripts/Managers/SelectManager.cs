using UnityEngine;

public class SelectManager : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.AddEvent<Collider>("OnRaycastHit", OnRaycastHit);
    }
    void OnDisable()
    {
        EventManager.RemoveEvent<Collider>("OnRaycastHit", OnRaycastHit);
    }

    void OnRaycastHit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<PlayerGui>().OpenCanvas();
        }
    }
}
