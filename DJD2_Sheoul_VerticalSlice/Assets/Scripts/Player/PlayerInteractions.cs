using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the player's interactions with objects in their
/// line of sight.
/// </summary>
public class PlayerInteractions : MonoBehaviour
{

    private const float MAX_INTERACTION_DISTANCE = 3.0f;

    public CanvasManager canvasManager;

    private Transform cameraTransform;
    private Interactable currentInteractive;
    private PlayerInventory playerInventory;

    /// <summary>
    /// Gets reference to camera and initializes an inventory for the player.
    /// </summary>
    public void Start()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
        playerInventory = new PlayerInventory(canvasManager);
    }

    /// <summary>
    /// Checks for an item in the line of sight and range, as well as 
    /// checking for an interaction input from the player every frame.
    /// </summary>
    public void Update()
    {
        CheckForInteractive();
        CheckForInteraction();
    }

    /// <summary>
    /// Uses raycasting to find the closest Interacteable in the center
    /// of the player's screen that is in range.
    /// </summary>
    private void CheckForInteractive()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward,
                out RaycastHit hitInfo, MAX_INTERACTION_DISTANCE))
        {
            Interactable newInteractive =
                hitInfo.collider.GetComponent<Interactable>() == null ?
                hitInfo.collider.GetComponentInParent<Interactable>() :
                hitInfo.collider.GetComponent<Interactable>();

            if (newInteractive != null && newInteractive != currentInteractive
                && !newInteractive.IsActive)
                SetCurrentInteractive(newInteractive);
            else if (newInteractive == null) ClearCurrentInteractive();
        }
        else
            ClearCurrentInteractive();
    }

    /// <summary>
    /// Stores the current Interactable that can be interacted with for use
    /// in the rest of the class.
    /// </summary>
    /// <param name="newInteractive"> The Interactable found by the raycast in CheckForInteractive()</param>
    private void SetCurrentInteractive(Interactable newInteractive)
    {
        currentInteractive = newInteractive;

        if (!currentInteractive.locked)
            canvasManager.ShowInteractionPanel(
                currentInteractive.interactText);
        else
            canvasManager.ShowInteractionPanel(
                currentInteractive.requirementText);
    }

    /// <summary>
    /// Clear the Current Interactive, and hide the interaction UI.
    /// </summary>
    private void ClearCurrentInteractive()
    {
        if (currentInteractive != null)
        {
            currentInteractive = null;
            canvasManager.HideInteractionPanel();
        }
    }

    /// <summary>
    /// Checks for player Input and acts on the current interactable
    ///  accordingly.
    /// </summary>
    private void CheckForInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentInteractive != null)
        {
            if ((currentInteractive as InventoryPickup) != null)
            {
                playerInventory.AddToInventory(
                    currentInteractive as InventoryPickup);

                (currentInteractive as InventoryPickup).Activate();

                currentInteractive.gameObject.SetActive(false);
            }

            else if (!currentInteractive.locked)
                currentInteractive.OnInteract(playerInventory);

            canvasManager.HideInteractionPanel();
        }
    }
}