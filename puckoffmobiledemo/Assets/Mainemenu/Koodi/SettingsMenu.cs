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
        audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("MVolume"));

        qualityDropDown.value = PlayerPrefs.GetInt(prefName, 3);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

}
