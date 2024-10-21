using UnityEngine;

public class HighlightManager : MonoBehaviour
{
    public GameObject selectionCube = null;

    void Awake()
    {
        selectionCube = Instantiate(selectionCube);
        selectionCube.SetActive(false);
    }
    void OnEnable()
    {
        EventManager.AddEvent<Collider>("OnRaycastHit", OnRaycastHit);
        EventManager.AddEvent("OnRaycastHitNull", OnRaycastHitNull);
        EventManager.AddEvent<bool>("OnLeftClick", OnLeftClick);
    }
    void OnDisable()
    {
        EventManager.RemoveEvent<Collider>("OnRaycastHit", OnRaycastHit);
        EventManager.RemoveEvent("OnRaycastHitNull", OnRaycastHitNull);
        EventManager.RemoveEvent<bool>("OnLeftClick", OnLeftClick);
    }

    void OnRaycastHit(Collider collider)
    {
        if (collider == null) return;

        if (collider.CompareTag("Player"))
        {
            selectionCube.transform.position = collider.transform.position;
            selectionCube.SetActive(true);
        }
        else
        {
            if (selectionCube != null && selectionCube.activeSelf)
                selectionCube.SetActive(false);
        }
    }

    void OnRaycastHitNull()
    {
        if (selectionCube != null && selectionCube.activeSelf)
            selectionCube.SetActive(false);
    }
    void OnLeftClick(bool stage)
    {

    }
}
