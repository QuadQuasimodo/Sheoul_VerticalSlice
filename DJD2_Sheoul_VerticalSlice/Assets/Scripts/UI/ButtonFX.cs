using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private const byte HOVER_ALPHA = 100;
    private const byte NORMAL_ALPHA = 255;

    private Color32 tempColor;
    private Text buttonText;

    private AudioSource audioSource;
    [SerializeField]private AudioClip hoverAudio;
    [SerializeField]private AudioClip clickAudio;

    void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        audioSource = GetComponent<AudioSource>();
        tempColor = buttonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tempColor.a = HOVER_ALPHA;
        buttonText.color = tempColor;

        audioSource.PlayOneShot(hoverAudio);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tempColor.a = NORMAL_ALPHA;
        buttonText.color = tempColor;
        audioSource.PlayOneShot(hoverAudio);
    }

    public void ResetColor()
    {
        tempColor.a = NORMAL_ALPHA;
        buttonText.color = tempColor;
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickAudio);
    }
}