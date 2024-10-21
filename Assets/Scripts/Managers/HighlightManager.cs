using UnityEngine;

public class HighlightManager : MonoBehaviour
{
    public static HighlightManager Instance;

    public GameObject selectionCube = null;
    public GameObject currentHighlightedPlayer = null;
    private bool highLightStage = true;

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

        selectionCube = Instantiate(selectionCube);
        selectionCube.SetActive(false);
    }
    void OnEnable()
    {
        EventManager.AddEvent<Collider>("OnRaycastHit", OnRaycastHit);
        EventManager.AddEvent("OnRaycastHitNull", OnRaycastHitNull);
        EventManager.AddEvent<bool>("OnHighlightStage", OnHighlightStage);
    }
    void OnDisable()
    {
        EventManager.RemoveEvent<Collider>("OnRaycastHit", OnRaycastHit);
        EventManager.RemoveEvent("OnRaycastHitNull", OnRaycastHitNull);
        EventManager.RemoveEvent<bool>("OnHighlightStage", OnHighlightStage);
    }

    void OnRaycastHit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (collider.gameObject != SelectManager.Instance.currentSelectedPlayer)
            {
                currentHighlightedPlayer = collider.gameObject;
                ShowCube(collider.transform);
            }
        }
        else
        {
            currentHighlightedPlayer = null;
            HideCube();
        }
    }

    void OnRaycastHitNull()
    {
        if (currentHighlightedPlayer != null)
            currentHighlightedPlayer = null;
        if (selectionCube != null && selectionCube.activeSelf)
            selectionCube.SetActive(false);
    }

    void ShowCube(Transform tf)
    {
        selectionCube.transform.position = tf.position;
        selectionCube.SetActive(true);
    }
    void HideCube()
    {
        if (selectionCube != null && selectionCube.activeSelf)
            selectionCube.SetActive(false);
    }

    void OnHighlightStage(bool stage)
    {
        highLightStage = stage;
        if (!highLightStage) HideCube();
    }
}
