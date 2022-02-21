using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    public Slider slider;           //hp slider
    public Gradient gradient;      //canvasin gradient
    public Image fill;            //kuva

    

    //otetaan Pelaajan maksimi health
    public void MaxHealth (int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    //asetetaan hp ja laitetaan hp väri sen mukaan
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }



}
