using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Handles the effects of the buttons
/// </summary>
public class ButtonFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Constant values for the alpha of the button color
    private const byte HOVER_ALPHA = 100;
    private const byte NORMAL_ALPHA = 255;

    // Temporary color (to be used by the buttonText)
    private Color32 tempColor;

    // Buttons text
    private Text buttonText;

    // Buttons audioSource
    private AudioSource audioSource;

    // AudioClips to use when the player hovers over
    // the buttons or clicks on them
    [SerializeField]private AudioClip hoverAudio;
    [SerializeField]private AudioClip clickAudio;

    /// <summary>
    /// Sets what the class does when it loads
    /// </summary>
    void Start()
    {
        // Gets the buttons text that's in its children
        buttonText = GetComponentInChildren<Text>();

        // Gets the audioScource component from itself
        audioSource = GetComponent<AudioSource>();

        // Sets the temporary color as the buttons current color
        tempColor = buttonText.color;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Sets the temporary colors alpha to the constant Hover_Alpha
        tempColor.a = HOVER_ALPHA;

        // Gives the text the temporary color
        buttonText.color = tempColor;

        // Plays one shot of the hoverAudio
        audioSource.PlayOneShot(hoverAudio);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Sets the temporary colors alpha to the constant Normal_Alpha
        tempColor.a = NORMAL_ALPHA;

        // Gives the text the temporary color
        buttonText.color = tempColor;

        // Plays one shot of the hoverAudio
        audioSource.PlayOneShot(hoverAudio);
    }

    /// <summary>
    /// Resets the color of the button
    /// </summary>
    public void ResetColor()
    {
        // Sets the temporary colors alpha to the Normal_Alpha
        tempColor.a = NORMAL_ALPHA;

        // Sets the button color to the temporary color
        buttonText.color = tempColor;
    }


    /// <summary>
    /// Plays a sound when the button is clicked
    /// </summary>
    public void PlayClickSound()
    {
        // Plays one shot of the given ClickAudio
        audioSource.PlayOneShot(clickAudio);
    }
}