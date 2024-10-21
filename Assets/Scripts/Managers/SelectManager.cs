using System;
using UnityEngine;

enum SelectMode { HOVER, SELECT }
public class SelectManager : MonoBehaviour
{
    public static SelectManager Instance;

    public GameObject selectionCube = null;
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
        EventManager.AddEvent<bool>("OnLeftClick", OnLeftClick);
    }
    void OnDisable()
    {
        EventManager.RemoveEvent<bool>("OnLeftClick", OnLeftClick);
    }


    void OnLeftClick(bool stage)
    {
        if (stage)
        {
            if (HighlightManager.Instance.currentHighlightedPlayer != null)
            {
                SelectManager.Instance.currentSelectedPlayer = HighlightManager.Instance.currentHighlightedPlayer;
                LookAt.Instance.ChangePlayer(HighlightManager.Instance.currentHighlightedPlayer.transform);
                EventManager.TriggerEvent("OnHighlightStage", false);
            }
        }
    }
}
