using UnityEngine;

public class SelectManager : MonoBehaviour
{
    public static SelectManager Instance;

    private GameObject currentSelectedPlayer = null;

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
            currentSelectedPlayer = collider.gameObject;
            collider.GetComponent<PlayerGui>().OpenCanvas();
        }
        else
        {
            if (currentSelectedPlayer != null)
            {
                GuiManager.Instance.CloseActivePlayerCanvas();
                currentSelectedPlayer = null;
            }
        }
    }
}
