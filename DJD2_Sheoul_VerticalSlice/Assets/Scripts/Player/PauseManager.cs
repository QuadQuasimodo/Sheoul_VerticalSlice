using UnityEngine;

/// <summary>
/// Handles the Pause Menu
/// </summary>
public class PauseManager : MonoBehaviour
{
    // Sets the key the user wants to pause the game
    [SerializeField] private KeyCode pauseKey;

    // Variable that tells if the game is paused or not
    public bool IsPaused { get; set; }

    // Player scripts to be disabled when the game is paused
    private PlayerInteractions pInteractions;
    private PlayerMovement pMovement;

    /// <summary>
    /// Sets what the class does when it loads
    /// </summary>
    void Start()
    {
        // Gets the player scripts instances form itself
        pInteractions = GetComponent<PlayerInteractions>();
        pMovement = GetComponent<PlayerMovement>();

        // Sets IsPaused to false;
        IsPaused = false;
    }

    /// <summary>
    /// Sets what the class does when its updated
    /// </summary>
    void Update()
    {
        // Calls OnPauseEnter if game is not paused and user presses pause key
        if (!IsPaused && Input.GetKeyDown(pauseKey)) OnPauseEnter();

        // Calls OnPauseExit if game is paused and user presses pause key
        else if (IsPaused && Input.GetKeyDown(pauseKey)) OnPauseExit();
    }

    /// <summary>
    /// Sets what need to be done when the player pauses the game
    /// </summary>
    public void OnPauseEnter()
    {
        // Sets IsPaused to true;
        IsPaused = true;

        // Disables other player scripts
        pInteractions.enabled = false;
        pMovement.enabled = false;

        // Unlocks cursor and makes it visible
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Activates the PausePanel
        pInteractions.canvasManager.ActivatePausePanel();
    }

    /// <summary>
    /// Sets what need to be done when the player unpauses the game
    /// </summary>
    public void OnPauseExit()
    {
        // Sets IsPaused to false;
        IsPaused = false;

        // Enables other player scripts
        pInteractions.enabled = true;
        pMovement.enabled = true;

        // Locks cursor and makes it invisible
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Deactivates the PausePanel
        pInteractions.canvasManager.DeactivatePausePanel();
    }

}
