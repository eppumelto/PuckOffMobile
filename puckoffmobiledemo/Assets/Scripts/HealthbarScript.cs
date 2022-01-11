using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    public Slider slider;
<<<<<<< HEAD:puckoffmobiledemo/Assets/HealthbarScript.cs
    public Gradient gradient;
    public Image fill; 
=======
    public float hp = 100;
>>>>>>> d521946480cba1e715b34210570d42c067830246:puckoffmobiledemo/Assets/Scripts/HealthbarScript.cs


    // Pelaajan maksimi health
    public void MaxHealth (int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }


    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void Update()
    {
        slider.value = hp; //paivittaa hp mittaria
    }

}
