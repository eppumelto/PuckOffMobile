using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown qualityDropDown;
    public Slider volSlider;
    

    const string prefName = "optionValue";

    private void Awake()
    {
        qualityDropDown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(prefName, qualityDropDown.value);
            PlayerPrefs.Save();
        }));
    }
    private void Start()
    {
        volSlider.value = PlayerPrefs.GetFloat("MVolume", 1f);
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("MVolume"));

        qualityDropDown.value = PlayerPrefs.GetInt(prefName, 3);

        if(PlayerPrefs.GetFloat("MVolume") == 0)
        {
            AudioListener.volume = 0;
        }

    }
    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("MVolume", volume);
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("MVolume"));

        if(volume == 0)
        {
            AudioListener.volume = 0;
        }

    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

}
