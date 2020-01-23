using UnityEngine;
using System.Collections.Generic;
//////////////////////// SIDE NOTE ////////////////////////

// For the effects of these items, 

///////////////////////////////////////////////////////////


/// <summary>
/// Class that inherits from 'Interactable', and defines
/// the behaviours of a 'InventoryPickup' interactable
/// </summary>
public class InventoryPickup : Interactable
{
    // Sets the 'Sprite' icon for the instance of 'InventoryPickup'
    public Sprite inventoryIcon;

    [HideInInspector]
    public List<Interactable> groupConsumers = new List<Interactable>();


    private void Awake()
    {
        foreach (Interactable e in MyInterGroup?.interGroup)
        {
            if (e.consumesFromInventory)
                groupConsumers.Add(e);
        }
    }

    /// <summary>
    /// Method that overrides 'Interactable' method 'Activate',
    /// that sets how a 'InventoryPickup' is activated - For this class,
    /// "activated" means it's in the player's Inventory
    /// </summary>
    public override void Activate()
    {
        IsActive = true;
        if (MyInterGroup != null) MyInterGroup.ActiveCount++;

        gameObject.SetActive(false);
    }
}
