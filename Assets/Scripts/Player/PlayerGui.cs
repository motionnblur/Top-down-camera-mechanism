using UnityEngine;

public class PlayerGui : MonoBehaviour
{
    [SerializeField] GameObject playerCanvas;
    public void OpenCanvas()
    {
        if (!playerCanvas.activeSelf)
        {
            playerCanvas.SetActive(true);
            GuiManager.Instance.OpenActivePlayerCanvas(playerCanvas);
        }
    }

    public void CloseCanvas()
    {
        if (playerCanvas.activeSelf)
        {
            playerCanvas.SetActive(false);
            GuiManager.Instance.CloseActivePlayerCanvas();
        }
    }
}
