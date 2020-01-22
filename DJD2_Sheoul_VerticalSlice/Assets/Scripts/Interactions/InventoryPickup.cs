using UnityEngine;
//////////////////////// SIDE NOTE ////////////////////////

// For the effects of these items, 

///////////////////////////////////////////////////////////


/// <summary>
/// Class that inherits from 'Interactable', and defines
/// the behaviours of a 'InventoryPickup' interactable
/// </summary>
public class InventoryPickup : Interactable
{
    // Sets name of the curren
    // public string inventoryName; - Delete if not in use

    // Sets the 'Sprite' icon for the instance of 'InventoryPickup'
    public Sprite inventoryIcon;

    /// <summary>
    /// Method that overrides 'Interactable' method 'Activate',
    /// that sets how a 'InventoryPickup' is activated - For this class,
    /// "activated" means it's in the player's Inventory
    /// </summary>
    public override void Activate()
    {
        // Sets bool 'IsActive' to true
        IsActive = true;

        // If the interactable belongs to an interaction group, increments
        // the value of 'ActiveCount (from the interaction group) by one
        if (MyInterGroup != null) MyInterGroup.ActiveCount++;
    }
}
