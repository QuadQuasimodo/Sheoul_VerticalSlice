using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    #region Interaction Variables

    public GameObject interactionPanel;
    public GameObject inventoryPanel;
    public Text interactionText;
    public Image[] inventoryIcons;

    #endregion

    public void Start()
    {
        pausePanel.SetActive(false);
        HideInteractionPanel();
    }

    #region Interaction Methods

    public void ShowInteractionPanel(string interactionMessage)
    {
        if (interactionMessage != "")
        {
            interactionText.text = interactionMessage;
            interactionPanel.SetActive(true);
        }
    }

    public void HideInteractionPanel() { interactionPanel.SetActive(false); }
    
    public void HideInventoryPanel() { inventoryPanel.SetActive(false); }
    
    public void ShowInventoryPanel() { inventoryPanel.SetActive(true); }

    public void ClearInventoryIcons()
    {
        for (int i = 0; i < inventoryIcons.Length; ++i)
        {
            inventoryIcons[i].sprite = null;
            inventoryIcons[i].color = Color.clear;
        }
    }

    public void SetInventoryIcon(int index, Sprite icon)
    {
        inventoryIcons[index].sprite = icon;
        inventoryIcons[index].color = Color.white;
    }
    #endregion

    #region Pause Methods
    public void ActivatePausePanel()
    {
        pausePanel.SetActive(true);
        inventoryPanel.SetActive(false);
    }
    public void DeactivatePausePanel()
    {
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(true);
    }
    #endregion
}
