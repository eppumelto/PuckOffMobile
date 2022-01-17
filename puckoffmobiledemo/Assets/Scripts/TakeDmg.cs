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

    private AIScript _aiScript;
    public HealthbarScript healthBar;
    private eventScript _eventScript;

    public int enemiesKilled;

    void Start()
    {
        PlayerAlive = true;
        firstime = true;

        currentHealth = maxHealth;
        healthBar.MaxHealth(maxHealth);
        isAlive = true;

        _aiScript = GameObject.FindWithTag("Enemy").GetComponent<AIScript>();
        _eventScript = GameObject.Find("ScriptManager").GetComponent<eventScript>();

        enemiesKilled = 0;
    }


    void Update()
    {
        

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            this.gameObject.SetActive(false);

            PlayerAlive = GameObject.Find("Pelaaja");
            if (PlayerAlive)
            {
                GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth += _aiScript.healtToPlayer;
            }


            Kuolema();
        }
   
        //kattoo jos haviaa pelin
        if (!PlayerAlive && firstime)
        {
            //eventScriptista voi muokata playerdead methodia tekemaan asiat
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

        enemiesKilled = +1;
        //_eventScript.EnemyDead(gameObject);
    }

   

}
