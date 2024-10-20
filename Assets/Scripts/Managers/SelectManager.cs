using UnityEngine;

public class SelectManager : MonoBehaviour
{
    public static SelectManager Instance;

    public GameObject currentSelectedPlayer = null;

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
            if (currentSelectedPlayer != null)
            {
                currentSelectedPlayer.GetComponent<PlayerGui>().CloseCanvas();
                currentSelectedPlayer = collider.gameObject;
                LookAt.Instance.ChangePlayer(collider.transform);
            }
            else
            {
                currentSelectedPlayer = collider.gameObject;
            }

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
