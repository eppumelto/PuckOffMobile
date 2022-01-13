using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDmg : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public static bool isAlive;
    
    //pelaajan kuolema
    public static bool PlayerAlive = true;
    private bool firstime;

    public HealthbarScript healthBar;
    private eventScript _eventScript;

    void Start()
    {
        PlayerAlive = true;
        firstime = true;

        currentHealth = maxHealth;
        healthBar.MaxHealth(maxHealth);
        isAlive = true;
        _eventScript = GameObject.Find("ScriptManager").GetComponent<eventScript>();
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
        if (!PlayerAlive && firstime)
        {
            //tanne tulee sitte panelit ja muut actiiviseks, slowmotion vois olla siisti
            _eventScript.PlayerDead();
            firstime = false;
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
