using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the canvas methods
/// </summary>
public class CanvasManager : MonoBehaviour
{
    // Sets Pause Panel
    [SerializeField] private GameObject pausePanel;

    #region Interaction Variables
    // Instance Variables to handle interactions
    public GameObject interactionPanel;
    public GameObject inventoryPanel;
    public Text interactionText;
    public Image[] inventoryIcons;

    #endregion

    /// <summary>
    /// Sets what the class does when it loads
    /// </summary>
    public void Start()
    {
        pausePanel.SetActive(false);
        HideInteractionPanel();
    }

    #region Interaction Methods

    /// <summary>
    /// Shows InteractionPanel and its message
    /// </summary>
    /// <param name="interactionMessage">Message to be displayed</param>
    public void ShowInteractionPanel(string interactionMessage)
    {
        // Checls if the message is not empty
        if (interactionMessage != "")
        {
            // Sets the interaction text to the given message
            interactionText.text = interactionMessage;

            // Activates the InteractionPanel
            interactionPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Hides the InteractionPanel
    /// </summary>
    public void HideInteractionPanel() { interactionPanel.SetActive(false); }

    /// <summary>
    /// Hides the InventoryPanel
    /// </summary>
    public void HideInventoryPanel() { inventoryPanel.SetActive(false); }

    /// <summary>
    /// Shows the InventoryPanel
    /// </summary>
    public void ShowInventoryPanel() { inventoryPanel.SetActive(true); }

    /// <summary>
    /// Clears the InventoryIcons
    /// </summary>
    public void ClearInventoryIcons()
    {
        // Loops through the InventoryIcons list
        for (int i = 0; i < inventoryIcons.Length; ++i)
        {
            // Sets the current inventoryIcon sprite to null
            inventoryIcons[i].sprite = null;

            // Clears the color of the current inventoryIcon
            inventoryIcons[i].color = Color.clear;
        }
    }

    /// <summary>
    /// Hides the InteractionPanel
    /// </summary>
    public void SetInventoryIcon(int index, Sprite icon)
    {
        inventoryIcons[index].sprite = icon;
        inventoryIcons[index].color = Color.white;
    }
    #endregion

    #region Pause Methods
    /// <summary>
    /// Activates the Pause Panel
    /// </summary>
    public void ActivatePausePanel()
    {
        // Makes PausePanel active if it isn't null
        pausePanel?.SetActive(true);

        // Makes InventoryPanel inactive if it isn't null
        inventoryPanel?.SetActive(false);
    }

    /// <summary>
    /// Deactivates the Pause Panel
    /// </summary>
    public void DeactivatePausePanel()
    {
        // Makes PausePanel inactive if it isn't null
        pausePanel?.SetActive(false);

        // Makes InventoryPanel active if it isn't null
        inventoryPanel?.SetActive(true);
    }
    #endregion
}
