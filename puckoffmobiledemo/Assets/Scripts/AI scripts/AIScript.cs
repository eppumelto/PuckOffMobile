using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIScript : MonoBehaviour
{

    //AI statseja
    public static HealthbarScript HealtScript;

    public static float healt;          //kertoo paljon AI lla on hp

    public float CoolDown;            //cooldown jota kaytetaan
    private float _originalCoolDown; //otetaan alkuperainen cooldown talteen

    public int dmg;                //paljon ai tekee dmg lyonnilla
    public int blockedDmg;        //paljon ai tekee dmg jos pelaaja blokkaa lyonnin
    

     //Defence ja fight juttuja
    private FightScript FightScript;
    public float PlayerStunTime; //kertoo kauan pelaaja stunaantuu kun osuu iskun
    public static float AIStunausAika; 

    private bool playerDef;                    //vastustajan block
    public static bool AiDefence;             //AI defence
    public float AiDefTime;                 //kertoo kauan AI suojaa
    
    public ParticleSystem blood;           //Pelaajan veri
    public ParticleSystem blockParticle;  //pelaajan blockp partikkeli

    public int healtToPlayer;           //kun ai kuolee antaa hp:ta pelaajalle
                                        /*    public GameObject PlayerHealtbar;*/  //Pelaajan healtbar


    //botin cooldown toimii sen perusteella suojaako se vai ei, jos se suojaa cooldown kestaa yhta kauan kun suojaus muuten se kestaa original cooldownin verran

    private Animator mAnimator;
    private Animator enemyAnimator;

    public CameraShake shake;

    private void Start()
    {
        _originalCoolDown = CoolDown;
        GameObject thePlayer = GameObject.Find("Pelaaja");

        blockParticle = thePlayer.transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
        blood = thePlayer.transform.GetChild(1).GetComponentInChildren<ParticleSystem>();

        mAnimator = GameObject.Find("Player").GetComponent<Animator>();
        enemyAnimator = GameObject.Find("Enemy").GetComponent<Animator>();

        shake = GameObject.Find("ScriptManager").GetComponent<CameraShake>();
    }






    public void agressive()
    {
        //isompi mahis lyoda
      int rnd = Random.Range(0, 10);

        if(rnd >= 3)
        {
            enemyAnimator.SetTrigger("EnemHit");
            //lyo
            if (!playerDef)
            {
                //PlayerHealtbar.GetComponent<HealthbarScript>().hp -= dmg;
                GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= dmg;
                blood.Play();
                mAnimator.SetTrigger("TakeDmg");
                FightScript.StunTime += PlayerStunTime; //Stunaa pelaajan pieneksi ajaksi
                shake.Effect1();

            }
            else if (playerDef)
            {
                blockParticle.Play(); //Lyonti suojattiin
               
                    GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= blockedDmg;
            }
           
        }
        //defendaa
        else if (rnd <= 2)
        {
            
            AiDefTime = 1.5f; // aloittaa suojauksen
            CoolDown = AiDefTime;
        }

        
       if(CoolDown <= 0)
        {
            CoolDown = _originalCoolDown;
        }

    }






    public void Defencive()
    {
        //Isompi mahis suojata
       int rnd = Random.Range(0, 10);

        if (rnd >= 7)
        {
            enemyAnimator.SetTrigger("EnemHit");

            if (!playerDef)
            {
   
                    GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= dmg;
                mAnimator.SetTrigger("TakeDmg");
                FightScript.StunTime += PlayerStunTime; //Stunaa pelaajan pieneksi ajaksi
                shake.Effect1();
                blood.Play();
            }
            else if (playerDef)
            {
                blockParticle.Play(); //Lyonti suojattiin
               
                    GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= blockedDmg;
                
            }

        }
        else if (rnd <= 6)
        {

            AiDefTime = 3.5f; // aloittaa suojauksen
            CoolDown = AiDefTime;
        }

        if (CoolDown <= 0)
        {
            CoolDown = _originalCoolDown;
        }


    }







    public void normal()
    {
        //tekee molempia yhta paljon
       int rnd = Random.Range(0, 10);

        if (rnd >= 5)
        {
            enemyAnimator.SetTrigger("EnemHit");
            if (!playerDef)
            {
                GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= dmg;
                blood.Play();
                shake.Effect1();
                mAnimator.SetTrigger("TakeDmg");
                FightScript.StunTime += PlayerStunTime; //Stunaa pelaajan pieneksi ajaksi
            }
            else if (playerDef)
            {
                blockParticle.Play(); //Lyonti suojattiin
                GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= blockedDmg;
            }

        }
        else if (rnd <= 4)
        {

            AiDefTime = 2f; // aloittaa suojauksen
            CoolDown = AiDefTime;
        }


        if (CoolDown <= 0)
        {
            CoolDown = _originalCoolDown;
        }


    }









    void Update()
    {
        //ai suojaa
        if(AiDefTime > 0)
        {
            enemyAnimator.SetTrigger("EnemyBlock");
            AiDefence = true;
            AiDefTime -= Time.deltaTime;
        }
        else
        {
            AiDefence = false;
            enemyAnimator.SetTrigger("EnemUnBlock");
        }


        playerDef = FightScript.block;

        healt = GameObject.FindWithTag("Enemy").GetComponent<TakeDmg>().currentHealth;
      

        //AI tappelee oman healtin mukaan.  Tarkistan etta vihu on oikealla kohdalla, ettei se puollusta ja cooldown on 0
        if (healt >= 70 && CoolDown <= 0 && !AiDefence && MoveToRightPos.cantHit && AIStunausAika <= 0 && TakeDmg.PlayerAlive)
        {
            agressive();
        }
        else if(healt < 70 && healt > 30 && CoolDown <= 0 && !AiDefence && MoveToRightPos.cantHit && AIStunausAika <= 0 && TakeDmg.PlayerAlive)
        {
            normal();
        }
        else if(healt <= 30 && CoolDown <= 0 && !AiDefence && MoveToRightPos.cantHit && AIStunausAika <= 0 && TakeDmg.PlayerAlive)
        {
            Defencive();
        }
 
        //jos AIStunattu
        if(AIStunausAika > 0)
        {
            AIStunausAika -= Time.deltaTime;
        }

        CoolDown -= Time.deltaTime;

        
    }
}
