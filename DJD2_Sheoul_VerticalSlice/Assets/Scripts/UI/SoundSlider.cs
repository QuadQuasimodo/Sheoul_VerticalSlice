using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    //public AudioListener aListener;

    private Slider volumeBar;
    private void Start()
    {
        AudioListener.volume = 5f;
        volumeBar = GetComponent<Slider>();
    }

    private void Update()
    {
        if (volumeBar.value < 1) volumeBar.value = 1;
        AudioListener.volume = volumeBar.value;
    }

    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
    }

}
