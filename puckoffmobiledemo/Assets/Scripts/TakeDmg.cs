using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDmg : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public static bool isAlive;
    private bool PlayerAlive = true;

    public HealthbarScript healthBar;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.MaxHealth(maxHealth);
        isAlive = true;
    }


    void Update()
    {
        

        healthBar.SetHealth(currentHealth);

        if (currentHealth == 0)
        {

            this.gameObject.SetActive(false);

            Kuolema();
            PlayerAlive = GameObject.Find("Pelaaja");

        }

        //kattoo jos haviaa pelin
        if (!PlayerAlive)
        {
            //tanne tulee sitte panelit ja muut actiiviseks, slowmotion vois olla siisti
            Debug.Log("Havisit");
            
        }


    }

    void Kuolema()
    {
        //Destroy(healthBar);
        //Destroy(this.gameObject);
        this.gameObject.SetActive(false);
        isAlive = false;
    }

   

}
