using UnityEngine;

public class GuiManager : MonoBehaviour
{
    public static GuiManager Instance { get; private set; }
    private GameObject currentActivePlayerCanvas;

    private void Awake()
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
    public void OpenActivePlayerCanvas(GameObject canvas)
    {
        if (canvas != null)
            currentActivePlayerCanvas = canvas;
    }
    public void CloseActivePlayerCanvas()
    {
        if (currentActivePlayerCanvas != null)
            currentActivePlayerCanvas.SetActive(false);
    }
}
