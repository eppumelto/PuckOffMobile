using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAIScript : MonoBehaviour
{
    //koodit joiden kanssa toimiii kivasti
    private FightScript _fightScript;


    //voit kertoa mika vihu on
    public bool normiVihu;
    public bool BOSS;

    public float coolDown;
         
    private float _originalCoolDown;

    //attack system
    public int EnemyDMG;
    public int blockedDmg;
    public float stunPlayerTime;

    //Defence system
    private float enemyDefTime;
    public static bool AiDefence;


    //Vihujen juttuja
    private Animator enemyAnimator;
    private int healt;
    public static float AIStunausAika;

    //pelaajan juttuja
    private Animator pAnimator;
    private CameraShake shake;
    private float playerHP;
    public ParticleSystem veri;
    public ParticleSystem torjunta;

    void Start()
    {
        _fightScript = GameObject.FindWithTag("Manager").GetComponent<FightScript>();

        _fightScript.theEnemy = this.gameObject; //kerrotaan nykyhetken vihu particleja varten
        _fightScript.enemyBlood = gameObject.transform.GetChild(2).GetComponentInChildren<ParticleSystem>();
       _fightScript.enemyBlock = gameObject.transform.GetChild(1).GetComponentInChildren<ParticleSystem>();


        //otetaan oikea cooldown
        _originalCoolDown = coolDown;
        coolDown = Mathf.Clamp(coolDown, 0, _originalCoolDown);
        


        //ai jutut
        enemyAnimator = gameObject.GetComponent<Animator>();
        veri = GameObject.Find("Pelaaja").transform.GetChild(1).GetComponentInChildren<ParticleSystem>();
        torjunta = GameObject.Find("Pelaaja").transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
        healt = GameObject.FindWithTag("Enemy").GetComponent<TakeDmg>().currentHealth;


        //pelaajan jutut
        pAnimator = GameObject.Find("Player").GetComponent<Animator>();
        shake = GameObject.Find("ScriptManager").GetComponent<CameraShake>();
       playerHP = GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth;
    }


    public void agressive()
    {
        //isompi mahis lyoda
        int rnd = Random.Range(0, 10);

        if (rnd >= 3)
        {
            enemyAnimator.SetTrigger("EnemHit");
            //lyo
            if (FightScript.block == false)
            {
                //PlayerHealtbar.GetComponent<HealthbarScript>().hp -= dmg;
                GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= EnemyDMG;
                veri.Play();
                pAnimator.SetTrigger("TakeDmg");
                FightScript.StunTime += stunPlayerTime; //Stunaa pelaajan pieneksi ajaksi
                shake.Effect1();

            }
            else if (FightScript.block == true)
            {
                torjunta.Play(); //Lyonti suojattiin

                GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= blockedDmg;
            }

        }
        //defendaa
        else if (rnd <= 2)
        {

           enemyDefTime = 1.5f; // aloittaa suojauksen
            coolDown = enemyDefTime;
            AiDefence = true;
        }


        if (coolDown <= 0)
        {
            coolDown = _originalCoolDown;
        }

    }


    public void Defencive()
    {
        //Isompi mahis suojata
        int rnd = Random.Range(0, 10);

        if (rnd >= 7)
        {
            enemyAnimator.SetTrigger("EnemHit");

            if (FightScript.block == false)
            {

                GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= EnemyDMG;
                pAnimator.SetTrigger("TakeDmg");
                FightScript.StunTime += stunPlayerTime; //Stunaa pelaajan pieneksi ajaksi
                shake.Effect1();
                veri.Play();
            }
            else if (FightScript.block)
            {
               torjunta.Play(); //Lyonti suojattiin

                GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= blockedDmg;

            }

        }
        else if (rnd <= 6)
        {

            enemyDefTime = 3.5f; // aloittaa suojauksen
            coolDown = enemyDefTime;
            AiDefence = true;
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
            enemyAnimator.SetTrigger("EnemHit");
            if (FightScript.block == false)
            {
                GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= EnemyDMG;
                veri.Play();
                shake.Effect1();
                pAnimator.SetTrigger("TakeDmg");
                FightScript.StunTime +=stunPlayerTime; //Stunaa pelaajan pieneksi ajaksi
            }
            else if (FightScript.block)
            {
                torjunta.Play(); //Lyonti suojattiin
                GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= blockedDmg;
            }

        }
        else if (rnd <= 4)
        {

            enemyDefTime = 2f; // aloittaa suojauksen
            coolDown = enemyDefTime;
            AiDefence = true;
        }


        if (coolDown <= 0)
        {
            coolDown = _originalCoolDown;
        }


    }






    public void BossAttack()
    {
        enemyAnimator.SetTrigger("Attack");
        //Tekee damagen pelaajaan sen perusteella suojasiko pelaaja iskun vai ei
        if (FightScript.block == true)
        {
            GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= blockedDmg;  //pelaaja suojasi iskun
            torjunta.Play();
        }
        else
        {
            GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= EnemyDMG;  //Pelaaja ei suojannut iskua
            veri.Play();
        }

        coolDown = _originalCoolDown;    //resettaa cooldownin
    }






    void Update()
    {

        if(normiVihu && !BOSS)
        {
            if (enemyDefTime > 0)
            {
                enemyAnimator.SetTrigger("EnemyBlock");
                AiDefence = true;
                enemyDefTime -= Time.deltaTime;
            }
            else
            {
                AiDefence = false;
                enemyAnimator.SetTrigger("EnemUnBlock");
            }


            //AI tappelee oman healtin mukaan.  Tarkistan etta vihu on oikealla kohdalla, ettei se puollusta ja cooldown on 0
            if (healt >= 70 && coolDown <= 0 && !AiDefence && MoveToRightPos.cantHit && AIStunausAika <= 0 && TakeDmg.PlayerAlive)
            {
                agressive();
            }
            else if (healt < 70 && healt > 30 && coolDown <= 0 && !AiDefence && MoveToRightPos.cantHit && AIStunausAika <= 0 && TakeDmg.PlayerAlive)
            {
                normal();
            }
            else if (healt <= 30 && coolDown <= 0 && !AiDefence && MoveToRightPos.cantHit && AIStunausAika <= 0 && TakeDmg.PlayerAlive)
            {
                Defencive();
            }


        }
        else if(BOSS && !normiVihu && coolDown <= 0 && TakeDmg.PlayerAlive)
        {
            BossAttack();
        }


        
        coolDown -= Time.deltaTime;



    }
}
