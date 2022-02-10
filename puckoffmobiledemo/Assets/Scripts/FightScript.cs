using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FightScript : MonoBehaviour
{
    

    //attack
    public float attackCooldown;
    private float CoolDown;
    public int Damage;
    public int BlockedDamage;
    public ParticleSystem enemyBlood;
    public float attackCount;

    //defence
    public static bool block;
    public ParticleSystem enemyBlock;
    public static float StunTime;
    public float PunchStunTime;


    public GameObject theEnemy;

    private float Defendingfloat;
    private Animator mAnimator;
    private Animator enemyAnimator;

    void Start()
    {
        //otetaan animaattorit vihusta ja pelaajasta
        enemyAnimator = GameObject.FindWithTag("Enemy").GetComponent<Animator>();
        mAnimator = GameObject.Find("Player").GetComponent<Animator>();

        //tallennan alkuperaisen cooldownin
        CoolDown = attackCooldown;
        attackCooldown = 0;
        theEnemy = GameObject.FindWithTag("Enemy");

        //otetaan particlet vastustajalta
        enemyBlock = theEnemy.transform.GetChild(1).GetComponentInChildren<ParticleSystem>();
        enemyBlood = theEnemy.transform.GetChild(2).GetComponentInChildren<ParticleSystem>();

        //TakeDmg takeDmg = thePlayer.GetComponent<TakeDmg>();

    }

  

    public void _attack()
    {
      

        //attackCooldown pitaa olla 0 ei suojaa ja vihu on oikealla paikalla
        if(attackCooldown <= 0 && !block && MoveToRightPos.cantHit && StunTime <= 0)
        {
            mAnimator.Rebind();
/*            mAnimator.SetTrigger("Punch"); */    //aloittaa animaation
            attackCooldown = CoolDown;        //resettaa cooldownin

            attackCount++;
            if (attackCount == 1)
            {
                Debug.Log("Moi");
                mAnimator.SetTrigger("Punch1");

            }
            attackCount = Mathf.Clamp(attackCount, 0, 3);
            if (attackCount >= 2)
            {
                Debug.Log("Moi2");
                mAnimator.SetTrigger("Punch2");
                attackCount = 0;
            }

            //tarkistaa ettei vastustaja suojaa
            if (SuperAIScript.AiDefence == false && !block)
            {
               //veri particle, miinustetaan hp vastustajalta, laitetaan animaatio
                enemyBlood.Play();
                SuperAIScript.AIStunausAika += PunchStunTime;
                GameObject.FindWithTag("Enemy").GetComponent<TakeDmg>().currentHealth -= Damage;
                

                enemyAnimator = GameObject.FindWithTag("Enemy").GetComponent<Animator>(); //otetaan animator
                enemyAnimator.Rebind();                                           //animator unohtaa aikaisemman animaation
                enemyAnimator.SetTrigger("EnemyDmg");                            //asettaa oikean animaation
               
            }
            else if(SuperAIScript.AiDefence == true)
            {
                enemyBlock.Play(); //lyonti blokattiin
                                   /*_hpBar.GetComponent<HealthbarScript>().hp -= BlockedDamage;*/ //tekee vahan dmg jos lyonti blokataan
               
                    GameObject.FindWithTag("Enemy").GetComponent<TakeDmg>().currentHealth -= BlockedDamage;

               
            }

           
        }

    }

   
    //Defence nappiin tarkistus onko se pohjassa vai ei samalla laitetaan animaatio
    //ei voi blokata jos sinua on juuri osuttu naamaan
    public void ButtonInHold()
    {
  
        
            block = true;

       
    }
    
    public void ButtonReleased()
    {
       
            block = false;
            
        
    }


  


    void Update()
    {
        theEnemy = GameObject.FindWithTag("Enemy");


        //Tarkistaa suojaako pelaaja ja jos pelaaja lyo niin ei voi suojata heti samaan aikaan
        if (block && Defendingfloat <= 0.11)
        {
            block = true;
            Defendingfloat += Time.deltaTime;
            mAnimator.SetFloat("Defending", Defendingfloat);
        }
        else if(!block && Defendingfloat > 0)
        {
            block = false;
            Defendingfloat -= Time.deltaTime;
            mAnimator.SetFloat("Defending", Defendingfloat);
        }

       
        if (TakeDmg.isAlive == false)
        {
            theEnemy = null;
            enemyBlock = null;
            enemyBlood = null;
        }

        else if (TakeDmg.isAlive == true)
        {
            enemyBlock = theEnemy.transform.GetChild(1).GetComponentInChildren<ParticleSystem>();
            enemyBlood = theEnemy.transform.GetChild(2).GetComponentInChildren<ParticleSystem>();
        }



        if(StunTime > 0)
        {
            StunTime -= Time.deltaTime;
        }

        attackCooldown -= Time.deltaTime;

    }
}
