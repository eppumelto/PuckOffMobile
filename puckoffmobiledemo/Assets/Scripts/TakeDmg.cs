using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDmg : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public static bool isAlive;

    public HealthbarScript healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.MaxHealth(maxHealth);
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        

        healthBar.SetHealth(currentHealth);

        if (currentHealth == 0)
        {

            this.gameObject.SetActive(false);

            Kuolema();
                  
        }
    }

    void Kuolema()
    {
        //Destroy(healthBar);
        //Destroy(this.gameObject);
        this.gameObject.SetActive(false);
        isAlive = false;
    }

    //public void TakeDamage(int damage)
    //{
    //    currentHealth -= damage;
    //}

}
