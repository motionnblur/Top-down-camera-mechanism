using UnityEngine;

enum SelectMode { HOVER, SELECT }
public class SelectManager : MonoBehaviour
{
    SelectMode currentMode = SelectMode.HOVER;

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
        switch (currentMode)
        {
            case SelectMode.HOVER:
                Hover(collider);
                break;
            case SelectMode.SELECT:
                Select(collider);
                break;
        }
    }

    void OnLeftClick(bool stage)
    {
        if (stage)
        {
            if (currentSelectionCube.activeSelf)
                currentSelectionCube.SetActive(false);

            currentMode = SelectMode.SELECT;
        }

    }

    void Hover(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (currentSelectionCube == null)
            {
                currentSelectionCube = Instantiate(selectionCube, collider.transform.position, Quaternion.identity);
            }
            else
            {
                currentSelectionCube.transform.position = collider.transform.position;
                if (!currentSelectionCube.activeSelf)
                    currentSelectionCube.SetActive(true);
            }
        }
        else
        {
            if (currentSelectionCube != null)
            {
                currentSelectionCube.SetActive(false);
            }
        }
    }
    void Select(Collider collider)
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
}
