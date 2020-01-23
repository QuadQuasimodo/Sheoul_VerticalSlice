using UnityEngine;
using System.Collections.Generic;

// for the effects of these items, Activated means it's in
// the player's Inventory

public class InventoryPickup : Interactable
{

    public string inventoryName;
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

    // Gets picked up and put in inventory
    public override void Activate()
    {
        IsActive = true;
        if (MyInterGroup != null) MyInterGroup.ActiveCount++;

        gameObject.SetActive(false);


    }
}
