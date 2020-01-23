using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory
{
    private readonly CanvasManager canvasManager;
   // public readonly List<InventoryPickup> inventory;

    private List<Interactable> inventoryConsumers;

    private Dictionary<InventoryPickup, List<Interactable>> pInventory;

    public PlayerInventory(CanvasManager cm)
    {
        canvasManager = cm;
        inventory = new List<InventoryPickup>();
        pInventory = new Dictionary<InventoryPickup, List<Interactable>>();
    }

    public void AddToInventory(InventoryPickup item)
    {
        inventory.Add(item);
        UpdateInventoryIcons();

        pInventory.Add(item, item.groupConsumers);
    }

    public void RemoveFromInventory(InventoryPickup item)
    {
        inventory.Remove(item);
        UpdateInventoryIcons();

        pInventory.Remove(item);
    }

    public bool HasInInventory(InventoryPickup item) =>
        pInventory.ContainsKey(item);

    public void UpdateInventoryIcons()
    {
        canvasManager.ClearInventoryIcons();

        for (int i = 0; i < inventory.Count; ++i)
            canvasManager.SetInventoryIcon(i, inventory[i].inventoryIcon);
    }

    public void CheckConsumeItem(Interactable currentInter)
    {
        InventoryPickup toRemove = null;

        foreach (KeyValuePair<InventoryPickup, List<Interactable>> entry in pInventory)
        {
            if (toRemove != null) break;
            for (int i = 0; i < entry.Value.Count; i++)
            {
                if (entry.Value[i] == currentInter)
                {
                    toRemove = entry.Key;
                    break;
                } 
            }

        }

        if(toRemove != null)
            RemoveFromInventory(toRemove);

    }
}
