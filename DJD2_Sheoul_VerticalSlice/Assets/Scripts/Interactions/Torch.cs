using UnityEngine;

/// <summary>
/// Class that inherits from 'Interactable', and defines
/// the behaviours of a 'Torch' interactable
/// </summary>
public class Torch : Interactable
{
    // Private 'GameObject' fire that will be set to
    // active when the player activate a 'Torch'
    [SerializeField] private GameObject Fire;


    private AudioSource audioSource;

    /// <summary>
    /// Activates the flame automatically on 'Awake',
    /// if the bool 'startsActive' is true
    /// </summary>
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (startsActive) Activate();
    }

    /// <summary>
    /// Method that overrides 'Interactable' method 'Activate',
    /// that sets how a 'Torch' is activated
    /// </summary>
    public override void Activate()
    {
        // Exits is tehe interactable is already active
        if (IsActive) return;

        // Sets the 'GameObject' Fire as active
        Fire.SetActive(true);

        // Sets the interactable variable 'IsActive' to true;
        IsActive = true;

        // If the interactable belongs to an interaction group, increments
        // the value of 'ActiveCount (from the interaction group) by one
        if(MyInterGroup != null) MyInterGroup.ActiveCount++;

        audioSource.Play();
    }
}
