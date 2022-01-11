using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    public Slider slider;

    public int maxHealth = 100;
    public int currentHealth;

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

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
