using UnityEngine;
using System.Collections;

/// <summary>
/// DO NOT USE THIS COMPONENT DIRECTLY.
/// USE THE SCRIPTS THAT INHERIT FROM THIS,     
///  Like torches, doors and things the player activates directly
/// </summary>
public abstract class Interactable : MonoBehaviour
{

    public string interactText;
    public string requirementText;

    // if the current interacteable is in its active state
    [HideInInspector] public bool IsActive { get; set; } = false;
    //public bool Active {get; set;}

    [Tooltip("Starts the scene already activated.")]
    // Starts the scene already activated
    public bool startsActive;

    [HideInInspector]
    public InteractionGroup MyInterGroup { get; set; } = null;

    [HideInInspector]
    public int GroupIndex { get; set; }

    [Tooltip("Is the player able to activate this right now.")]
    // Is this item activatable by the player right now
    public bool locked = false;

    [Tooltip("Does this need all others from it's interaction group " +
    "to be activated for itself to be activateable")]
    // Does this object need all others from its group to be activated for
    // itself to be activatable
    public bool requiresOthersFromGroup = false;

    [Tooltip("Activates automatically once every object in group is active")]
    ///////// INSERT MESSAGE HERE /////////
    public bool activatesAutomatically = false;

    [Tooltip("Other items in the group can activate this one")]
    // Allow other items in the interactiongroup to trigger
    // this item's active state
    [SerializeField] private bool activateableByOtherFromGroup = true;


    /// <summary>
    /// Triggers the object's Active state and does whatever the object would do
    /// once interacted with in a valid way.
    /// </summary>
    public abstract void Activate();

    /// <summary>
    /// Called when player interacts with this
    /// </summary>
    /// <param name="simult"> if all in group are activated at the same
    /// time or if there is a slow chain effect </param>
    public virtual void OnInteract(PlayerInventory playerInventory)
    {
        if (MyInterGroup == null)
        {
            Activate();
            return;
        }

        if (locked) return;

        else if (!locked) Activate();

        if (requiresOthersFromGroup)
            for (int i = 0; i < MyInterGroup.interGroup.Count; i++)
            {
                playerInventory.RemoveFromInventory(
                    MyInterGroup.interGroup[i] as InventoryPickup); 
            }

        if (MyInterGroup.activChainType ==
            InteractionGroup.ActivationChainTypes.Simultaneous)
            SimultaneousActivation();

        else if (MyInterGroup.activChainType ==
            InteractionGroup.ActivationChainTypes.Ping_Pong)
            StartCoroutine(PingPongActivation());

        else if (MyInterGroup.activChainType ==
            InteractionGroup.ActivationChainTypes.Simetrical)
            StartCoroutine(SimmetricalActivation());
    }

    /// <summary>
    /// Activates all at once. 
    /// </summary>
    private void SimultaneousActivation()
    {
        foreach (Interactable i in MyInterGroup.interGroup)
        {
            if (i.activateableByOtherFromGroup)
                i.Activate();
        }
    }

    /// <summary>
    /// Activates alternating between higher and lower index of the 
    /// previously activated.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PingPongActivation()
    {
        int i, j;
        for (i = j = GroupIndex; (i < MyInterGroup.interGroup.Count)
            || (j >= 0); i++, j--)
        {
            if (i < MyInterGroup.interGroup.Count)
            {
                if (MyInterGroup.interGroup[i].
                    activateableByOtherFromGroup)
                {
                    yield return new WaitForSeconds(MyInterGroup.activationDelay);
                    MyInterGroup.interGroup[i].Activate();
                }
            }
            if (j >= 0)
            {
                if (MyInterGroup.interGroup[j].
                    activateableByOtherFromGroup)
                {
                    yield return new WaitForSeconds(MyInterGroup.activationDelay);
                    MyInterGroup.interGroup[j].Activate();
                }
            }
        }
    }

    /// <summary>
    /// Activates both the highest and lowest index of objects in relation
    /// with the object originally activated.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SimmetricalActivation()
    {
        int i, j;
        for (i = j = GroupIndex; (i < MyInterGroup.interGroup.Count) || (j >= 0); i++, j--)
        {
            yield return new WaitForSeconds(MyInterGroup.activationDelay);

            if (i < MyInterGroup.interGroup.Count)
            {
                if (MyInterGroup.interGroup[i].activateableByOtherFromGroup)
                    MyInterGroup.interGroup[i].Activate();
            }
            if (j >= 0)
            {
                if (MyInterGroup.interGroup[j].activateableByOtherFromGroup)
                    MyInterGroup.interGroup[j].Activate();
            }
        }
    }
}

