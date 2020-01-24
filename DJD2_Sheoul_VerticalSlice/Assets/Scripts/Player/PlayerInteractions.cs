using UnityEngine;
using System.Collections;

public class PlayerInteractions : MonoBehaviour
{

    private const float MAX_INTERACTION_DISTANCE = 3.0f;

    public CanvasManager canvasManager;

    private Transform cameraTransform;
    private Interactable currentInteractive;
    private PlayerInventory playerInventory;


    public void Start()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
        playerInventory = new PlayerInventory(canvasManager);
    }

    public void Update()
    {
        CheckForInteractive();
        CheckForInteraction();
    }

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

    private void ClearCurrentInteractive()
    {
        if (currentInteractive != null)
        {
            currentInteractive = null;
            canvasManager.HideInteractionPanel();
        }
    }

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