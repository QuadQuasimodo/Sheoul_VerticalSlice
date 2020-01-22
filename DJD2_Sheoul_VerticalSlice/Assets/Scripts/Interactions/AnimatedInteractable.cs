using UnityEngine;

/// <summary>
/// Class that inherits from 'Interactable', and defines
/// the behaviours of a 'AnimatedInteractable' interactable
/// </summary>
public class AnimatedInteractable : Interactable
{
    // Private 'Animator' animator that sets the
    // animator that the 'AnimatedInteractable' uses when it activates
    private Animator animator;

    // Method 'Awake' that defines what the object does on Awake
    private void Awake()
    {
        // Sets the instances animator as the animator on the GameObject
        animator = GetComponent<Animator>();

        // Calls method activate automatically
        // if the variable 'startsActive' is true
        if (startsActive) Activate();
    }

    /// <summary>
    /// Method that overrides 'Interactable' method 'Activate',
    /// that sets how a 'AnimatedInteractable' is activated
    /// </summary>
    public override void Activate()
    {
        // Exits is tehe interactable is already active
        if (IsActive) return;

        // Sets the value of the "Interacted" trigger parameter of the animator
        animator.SetTrigger("Interacted");

        // Sets the interactable variable 'IsActive' to true;
        IsActive = true;
    }
}
