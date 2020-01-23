using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTextOpacity : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private const byte HOVER_ALPHA = 100;
    private const byte NORMAL_ALPHA = 255;

    private Color32 tempColor;
    private Text buttonText;

    void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        tempColor = buttonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tempColor.a = HOVER_ALPHA;
        buttonText.color = tempColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tempColor.a = NORMAL_ALPHA;
        buttonText.color = tempColor;
    }

    public void ResetColor()
    {
        tempColor.a = NORMAL_ALPHA;
        buttonText.color = tempColor;
    }
}