using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class that stores a list of objects that will interact with each other.
/// Has information on the ordering and method of activation
/// </summary>
public class InteractionGroup : MonoBehaviour
{
   
   /// <summary>
   /// Simultaneous: Activates all at once.
   /// Ping_Pong: Activates alternating between higher and lower index of the
   /// previously activated.
   /// Simetrical: Activates both the highest and lowest index of objects in
   /// relation with the object originally activated.
   /// </summary>
    public enum ActivationChainTypes {Simultaneous, Ping_Pong, Simetrical }

    [Tooltip("Objects that will interact with each other," +
    "or otherwise are related when it comes to interactions or activations.")]
    public List<Interactable> interGroup = new List<Interactable>();

    [Tooltip("Delay between each item being activated in the group")]
    public float activationDelay;

    [Tooltip("The chain reaction has can only be started by a certain object")]
    public bool specificReactionStarter = false;

    
    [Tooltip("Index of Object that is the starter")]
    public int indexOfStarterOb;

    [Tooltip("The way the group will activate in the chain.")]
    public ActivationChainTypes activChainType;


    public int ActiveCount { get; set; } = 0;

    /// <summary>
    /// Tells every object in the group about this instance of the class.
    /// Also does preliminary setup to lock some objects from being activated.
    /// </summary>
    private void Awake()
    {
        indexOfStarterOb = Mathf.Clamp(indexOfStarterOb, 0, interGroup.Count);
        for(int i = 0; i<interGroup.Count; i++)
        {
            interGroup[i].MyInterGroup = this;
            interGroup[i].GroupIndex = i;

            if ((specificReactionStarter && i != indexOfStarterOb) ||
                interGroup[i].requiresOthersFromGroup) 
            {
                interGroup[i].locked = true;
            }
        }
    }

    /// <summary>
    /// Activates objects that wait for others in the group to be active.
    /// </summary>
    private void Update()
    {
        for (int i = 0; i < interGroup.Count; i++)
        {
            if (interGroup[i].locked)
            {
                if (ActiveCount == interGroup.Count - 1)
                {
                    interGroup[i].locked = false;
                    if (interGroup[i].activatesAutomatically)
                        interGroup[i].Activate();
                }
            }
        }
    }
}
