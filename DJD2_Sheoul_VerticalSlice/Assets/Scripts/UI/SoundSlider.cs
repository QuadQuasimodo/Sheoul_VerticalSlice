using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Defines the behaviour of the Slider that handles the sound volume
/// </summary>
public class SoundSlider : MonoBehaviour
{
    // Slider used to manipulate game volume
    private Slider volumeBar;

    /// <summary>
    /// Sets what need to be done when the class is loaded
    /// </summary>
    private void Start()
    {
        // Sets AudioListener volume to 1
        AudioListener.volume = 1f;

        // Gets the component Slider from itself
        volumeBar = GetComponent<Slider>();
    }

    /// <summary>
    /// Sets what needs to be done continuously by this script
    /// </summary>
    private void Update()
    {
        // Sets the Slider value to 1 if it's less than 1
        if (volumeBar.value < 1) volumeBar.value = 1;

        // Sets the AudioListener volume to the value of the Slider
        AudioListener.volume = volumeBar.value;
    }

    /// <summary>
    /// Method that allows components to access
    /// and change the AudioListener volume
    /// </summary>
    /// <param name="volume">Volume to be set to the AudioListener</param>
    public void ChangeVolume(float volume)
    {
        // Sets AudioListener volume to the given volume
        AudioListener.volume = volume;
    }

}
