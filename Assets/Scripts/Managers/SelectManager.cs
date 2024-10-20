using UnityEngine;

public class SelectManager : MonoBehaviour
{
    public static SelectManager Instance;

    public GameObject selectionCube = null;
    public GameObject currentSelectedPlayer = null;

    private GameObject currentSelectionCube = null;
    private bool rayLocked = false;

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
        EventManager.AddEvent<bool>("OnLeftClick", OnLeftClick);
    }
    void OnDisable()
    {
        EventManager.RemoveEvent<Collider>("OnRaycastHit", OnRaycastHit);
        EventManager.AddEvent<bool>("OnLeftClick", OnLeftClick);
    }

    void OnRaycastHit(Collider collider)
    {
        if (rayLocked) return;

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

            if (currentSelectionCube == null)
            {
                currentSelectionCube = Instantiate(selectionCube, collider.transform.position, Quaternion.identity);
            }
            else
            {
                currentSelectionCube.transform.position = collider.transform.position;
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
        rayLocked = true;
    }

    void OnLeftClick(bool stage)
    {
        if (!stage)
            rayLocked = false;
    }
}
