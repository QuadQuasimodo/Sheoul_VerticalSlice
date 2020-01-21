using UnityEngine;
using System.Collections.Generic;

public class InteractionGroup : MonoBehaviour
{
   
    public enum ActivationChainTypes {Simultaneous, Ping_Pong, Simetrical }

    [Tooltip("Objects that will interact with each other," +
    "or otherwise are related when it comes to interactions or activations.")]
    public List<Interactable> interGroup = new List<Interactable>();

    [Tooltip("Delay between each item being activated in the group")]
    public float activationDelay;

   /* [Tooltip("All objects in group activate at the same time.")]
    public bool SimultaneousActivation = false;

    public bool PingPongActivation = true;

    public bool ChainActivation = false;*/

    [Tooltip("The chain reaction has can only be started by a certain object")]
    public bool specificReactionStarter = false;

    
    [Tooltip("Index of Object that is the starter")]
    public int indexOfStarterOb;

    [Tooltip("The way the group will activate in the chain.")]
    public ActivationChainTypes activChainType;


    public int ActiveCount { get; set; } = 0;
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
