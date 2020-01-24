using System.Collections.Generic;

/// <summary>
/// Class that will manage all objects in the Player's 
/// Inventory.
/// </summary>
public class PlayerInventory
{

    private readonly CanvasManager canvasManager;
    public readonly List<InventoryPickup> inventory;

    public PlayerInventory(CanvasManager cm)
    {
        canvasManager = cm;
        inventory = new List<InventoryPickup>();
    }

    /// <summary>
    /// Adds item to the the list of items in the inventory.
    /// </summary>
    /// <param name="item">Item to add</param>
    public void AddToInventory(InventoryPickup item)
    {
        inventory.Add(item);
        UpdateInventoryIcons();
    }

    /// <summary>
    /// Removes item from the player's inventory.
    /// </summary>
    /// <param name="item">Item to remove.</param>
    public void RemoveFromInventory(InventoryPickup item)
    {
        inventory.Remove(item);
        UpdateInventoryIcons();
    }

    /// <summary>
    /// Calls canvasManager to update the icons in the hotbar.
    /// </summary>
    public void UpdateInventoryIcons()
    {
        canvasManager.ClearInventoryIcons();

        for (int i = 0; i < inventory.Count; ++i)
            canvasManager.SetInventoryIcon(i, inventory[i].inventoryIcon);
    }
}
