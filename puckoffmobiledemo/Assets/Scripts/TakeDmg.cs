using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDmg : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private float DesPawnTime = 2;

    public static bool isAlive;
    private bool firstDeath = true;
    public bool isLast; //kertoo onko vastustaja viimeinen
    
    //pelaajan kuolema
    public static bool PlayerAlive = true;
    private float _playerHealt;
    private bool firstime;

    private AIScript _aiScript;
    public HealthbarScript healthBar;
    private eventScript _eventScript;

    public static int enemiesKilled;

    private Animator mAnimator;
    private Animator enemyAnimator;

    public SpriteRenderer enemyHead;
    public Sprite[] headSprites;

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

        enemyHead = GameObject.Find("enemyHeadChanger").GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        HeadChange();
       //pieni cooldown vihun kuolemaan
        if (!firstDeath && DesPawnTime <= 100f)
        {

            DesPawnTime -= Time.deltaTime;

            if(DesPawnTime <= -1)
            {
                this.gameObject.SetActive(false);
                isAlive = false;
                enemiesKilled = enemiesKilled + 1;

            }

        }


        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0 && firstDeath)
        {
            firstDeath = false;

            



            //tarkistetaan onko pelaaja hengissa ja jos on annetaan pieni healt regen pelaajalle
            //PlayerAlive
            _playerHealt = GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth;
            if (_playerHealt > 0)
            {
                //tarkistan onko se vika vihu vai boss kenet tapetaan
                if (this.gameObject.GetComponent<TakeDmg>().isLast == true)
                {
                    _eventScript.PlayerWON();
                }
                
                GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth += _aiScript.healtToPlayer;
                //otetaan ai koodissa defence pois ja laitetaan vastustajan animaatioksi kuolema
                _aiScript.AiDefTime = 0;
                enemyAnimator.Rebind();
                enemyAnimator.SetTrigger("Die");

                //Vaihdetaan vastustajan paa ja kutsutaan Kuolema methdoia
                enemyHead.sprite = headSprites[0];
                Kuolema();
            }
            else if (_playerHealt <= 0)
            {
                //Pelaaja kuoli joten kutsutaan method PlayerDead ja laitetaan bool falseksi
                PlayerAlive = false;
                _eventScript.PlayerDead();
            }
            

            
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
        enemyAnimator = GameObject.Find("Enemy").GetComponent<Animator>();

        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {


                this.gameObject.SetActive(false);
                isAlive = false;
                enemiesKilled = enemiesKilled + 1;



        }
        else
        {
            DesPawnTime = 0;
        }


        

    }

   public void HeadChange()
    {

        if (this.tag == "Enemy")
        {

            if (currentHealth <= 60 && currentHealth > 20)
            {
                enemyHead.sprite = headSprites[1];               
            }
            else if (currentHealth <= 20 && currentHealth > 0)
            {
                enemyHead.sprite = headSprites[2];
            }
        }


    }

}
