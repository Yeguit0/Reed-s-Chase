using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [Header("Volumen")]
    public Slider volumeSlider;

    void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 1f);

        volumeSlider.value = volume;
        AudioListener.volume = volume;

        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;

        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }
}
