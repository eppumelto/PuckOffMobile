using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    public Slider slider;
    public float hp = 100;
    

    // Pelaajan maksimi health
    public void MaxHealth (int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }


    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void Update()
    {
        slider.value = hp; //paivittaa hp mittaria
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
