using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDmg : MonoBehaviour
{
    public int maxHealth = 100;
    public static int currentHealth;

    public HealthbarScript healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.MaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        

        healthBar.SetHealth(currentHealth);

        if (currentHealth == 0)
        {
            Destroy(healthBar);
            Destroy(this.gameObject);            
        }
    }

    //public void TakeDamage(int damage)
    //{
    //    currentHealth -= damage;
    //}

}
