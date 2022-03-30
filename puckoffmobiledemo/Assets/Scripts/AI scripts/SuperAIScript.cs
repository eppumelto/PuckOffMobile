using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAIScript : MonoBehaviour
{
    //koodit joiden kanssa toimiii kivasti
    private FightScript _fightScript;


    //voit kertoa Unityssa milla tyylilla vastustaja tappelee
    public bool normiVihu;
    public bool BOSS;

    //cooldown ja alkuperainen cooldown
    public float coolDown;
    private float _originalCoolDown;

    //Attack system
    public int EnemyDMG;              //dmg jota vihu tekee jos pelaaja ei suojaa
    public int blockedDmg;           //dmg jos pelaaja suojaa
    public float stunPlayerTime;    //aika kauaksi aikaa pelaaja stunaantuu jos osuu

    //Defence system
    private float enemyDefTime;         //kertoo kauan vastustaja joutuu suojaamaan

    //kertoo kauan vastustaja suojaa eri tilanteissa
    public float AgreDeftime;       //agressiivinen
    public float DefenDeftime;     //defensive
    public float normalDeftime;   //normal

    public static bool AiDefence;      //kertoo suojaako vastustaja

    //Vihujen tietoja ja componentteja
    private Animator enemyAnimator;         //vastustajan animator
    private int healt;                     //vastustajan healt
    public static float AIStunausAika;    //Kertoo onko vihu stunattu
    public int GiveHealt;

    //pelaajan tietoja ja componentteja
    private Animator pAnimator;             //pelaajan animator
    private CameraShake shake;             //kameraan ns effect
    private float playerHP;               //pelaajan hp
    public ParticleSystem veri;          //veri particle
    public ParticleSystem torjunta;     //torjunta particle

    //aania
    //public AudioSource BlockSound; 

    void Start()
    {
        _fightScript = GameObject.FindWithTag("Manager").GetComponent<FightScript>(); //otetaan manager jotta voidaan vaihtaa particlet


        _fightScript.theEnemy = this.gameObject; //kerrotaan nykyhetken vihu particleja varten
        _fightScript.enemyBlood = gameObject.transform.GetChild(2).GetComponentInChildren<ParticleSystem>(); //otetaan veri particle
       _fightScript.enemyBlock = gameObject.transform.GetChild(1).GetComponentInChildren<ParticleSystem>(); //otetaan block particle
       

        //otetaan oikea cooldown
        _originalCoolDown = coolDown;
        coolDown = Mathf.Clamp(coolDown, 0, _originalCoolDown);
        


        //Otetaan pelaajan particlet ja vastustajan hp
        enemyAnimator = gameObject.GetComponent<Animator>();           //otetaan animator

        _fightScript.enemyAnimator = enemyAnimator;

        veri = GameObject.Find("Pelaaja").transform.GetChild(1).GetComponentInChildren<ParticleSystem>();       //veri particle
        torjunta = GameObject.Find("Pelaaja").transform.GetChild(0).GetComponentInChildren<ParticleSystem>();  //block particle
        healt = GameObject.FindWithTag("Enemy").GetComponent<TakeDmg>().currentHealth;                        //vihun healt


        //pelaajan animator ja hp, kamera effect
        pAnimator = GameObject.Find("Player").GetComponent<Animator>();                     //pelaajan animator
        shake = GameObject.Find("ScriptManager").GetComponent<CameraShake>();              //otetaan kamera ravistelua varten
       playerHP = GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth;       //pelaajan hp

        FightScript.block = false;

        
    }

    public void attack()
    {
        //jos pelaaja ei suojaa
        if (FightScript.block == false)
        {
            
            GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= EnemyDMG;   //tekee damagea pelaajaan

            veri.Play();   //veri particle
            shake.Effect1();    //kamera effect
            pAnimator.SetTrigger("TakeDmg");         //pelaajan animaatio
            FindObjectOfType<AudioManager>().Play("Punch1");
            FightScript.StunTime += stunPlayerTime; //Stunaa pelaajan pieneksi ajaksi

        }
        //jos pelaaja suojaa
        else if (FightScript.block == true)
        {
            torjunta.Play(); //suojaus particle
            FindObjectOfType<AudioManager>().Play("Block");

            GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= blockedDmg;     //tekee damagea pelaajaan
        }

    }


    public void agressive()
    {
        //Agressive, eli arpoo random numeron ja jos luku on isompi kuin 2 vastustaja iskee muuten vastustaja suojaa
        int rnd = Random.Range(0, 10);


        //lyö
        if (rnd >= 3)
        {
            enemyAnimator.SetTrigger("Attack"); //lyomis animaatio


           
        }
        //defendaa
        else if (rnd <= 2)
        {
            enemyDefTime = normalDeftime; // aloittaa suojauksen
            coolDown = enemyDefTime;

        }


        if (coolDown <= 0)
        {
            coolDown = _originalCoolDown;
        }

    }


    public void Defencive()
    {
        //Isompi mahis suojata muuten sama kuin Agressive
        int rnd = Random.Range(0, 10);

        if (rnd >= 7)
        {
            enemyAnimator.SetTrigger("Attack");
  


            

        }
        else if (rnd <= 6)
        {

            enemyDefTime = normalDeftime; // aloittaa suojauksen
            coolDown = enemyDefTime;

        }

        if (coolDown <= 0)
        {
            coolDown = _originalCoolDown;
        }


    }







    public void normal()
    {
        //tekee molempia yhta paljon
        int rnd = Random.Range(0, 10);

        if (rnd >= 5)
        {
            enemyAnimator.SetTrigger("Attack");
        
           
        }
        else if (rnd <= 4)
        {

            enemyDefTime = normalDeftime; // aloittaa suojauksen
            coolDown = enemyDefTime;

        }


        if (coolDown <= 0)
        {
            coolDown = _originalCoolDown;
        }


    }





    //eka bossi on aivokuollut ja siksi se osaa vain iskeä
    public void BossAttack()
    {
        


     

            //Tekee damagen pelaajaan sen perusteella suojasiko pelaaja iskun vai ei
            if (FightScript.block == true)
            {
                GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= blockedDmg;  //pelaaja suojasi iskun
                torjunta.Play();
            //torjunta particle
            FindObjectOfType<AudioManager>().Play("Block");
        }
            else
            {
                GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= EnemyDMG;  //Pelaaja ei suojannut iskua
                veri.Play();                                                                  //veri particle
            }

        coolDown = _originalCoolDown;    //resettaa cooldownin
    }






    void Update()
    {
        //katotaan, että onko normi vihu vai boss
        if (normiVihu && !BOSS)
        {
            //jos vastustaja suojaus aika on isompi kuin 0 silloin se suojaa
            if (enemyDefTime > 0)
            {   

                enemyAnimator.SetTrigger("EnemyBlock");     //block animaatio
                AiDefence = true;                          //suojaa bool true
                enemyDefTime -= Time.deltaTime;           //otetaan aikaa pois suojauksesta
            }
            else 
            {
                AiDefence = false;                          //ei suojaa
                enemyAnimator.SetTrigger("EnemyUnBlock");  //animaatio pois
            }


            //AI tappelee oman healtin mukaan.  Tarkistan etta vihu on oikealla kohdalla, ettei se puollusta ja cooldown on 0
            if (coolDown <= 0 && !AiDefence && MoveToRightPos.cantHit && AIStunausAika <= 0 && TakeDmg.PlayerAlive && GetComponent<TakeDmg>().currentHealth >= 70)
            {
                agressive();

            }
            else if (coolDown <= 0 && !AiDefence && MoveToRightPos.cantHit && AIStunausAika <= 0 && TakeDmg.PlayerAlive && GetComponent<TakeDmg>().currentHealth < 70 && GetComponent<TakeDmg>().currentHealth > 30)
            {
                normal();

            }
            else if (coolDown <= 0 && !AiDefence && MoveToRightPos.cantHit && AIStunausAika <= 0 && TakeDmg.PlayerAlive && GetComponent<TakeDmg>().currentHealth <= 30)
            {
                Defencive();

            }

         
        }
        
           //sama kuin aikaisempi, mutta boss systeemillä
        if(BOSS && !normiVihu && coolDown <= 0 && TakeDmg.PlayerAlive && MoveToRightPos.cantHit == true)
        {
            enemyAnimator.SetTrigger("Attack"); //Boss attack animaatio alkaa
        
        }
        else if(BOSS && coolDown > 0 && TakeDmg.PlayerAlive && MoveToRightPos.cantHit == true)
        {
            Debug.Log(coolDown);
        }
        


        
        coolDown -= Time.deltaTime;



    }
}
