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

    public static int enemiesKilled;

    private Animator mAnimator;
    private Animator enemyAnimator;

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

        mAnimator = GameObject.Find("Player").GetComponent<Animator>();
        enemyAnimator = GameObject.Find("Enemy").GetComponent<Animator>();
    }


    void Update()
    {
        

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            //this.gameObject.SetActive(false);

            PlayerAlive = GameObject.Find("Pelaaja");
            if (PlayerAlive)
            {
                GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth += _aiScript.healtToPlayer;
                //otetaan ai koodissa defence pois
                _aiScript.AiDefTime = 0;
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
        MoveToRightPos.cantHit = false;
       Destroy(GameObject.FindWithTag("Enemy").GetComponent<AIScript>());
        enemyAnimator.SetTrigger("Die");
        Debug.Log("Moi");
        
        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {


            Debug.Log("´Hehheee");
                this.gameObject.SetActive(false);
                isAlive = false;
                enemiesKilled = enemiesKilled + 1;

            

        }







        

        
        //_eventScript.EnemyDead(gameObject);
        

    }

   

}
