using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private KeyCode pauseKey;

    public bool IsPaused { get; set; }

    private PlayerInteractions pInteractions;
    private PlayerMovement pMovement;

    // Start is called before the first frame update
    void Start()
    {
        pInteractions = GetComponent<PlayerInteractions>();
        pMovement = GetComponent<PlayerMovement>();
        IsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsPaused && Input.GetKeyDown(pauseKey)) OnPauseEnter();
        else if (IsPaused && Input.GetKeyDown(pauseKey)) OnPauseExit();
    }

    public void OnPauseEnter()
    {
        IsPaused = true;
        pInteractions.enabled = false;
        pMovement.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pInteractions.canvasManager.ActivatePausePanel();
    }

    public void OnPauseExit()
    {
        IsPaused = false;
        pInteractions.enabled = true;
        pMovement.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pInteractions.canvasManager.DeactivatePausePanel();
    }

}
