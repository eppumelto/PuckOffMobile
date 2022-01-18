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

    //defence
    public static bool block;
    public ParticleSystem enemyBlock;

    public GameObject theEnemy;

    private Animator mAnimator;

    void Start()
    {   


        mAnimator = GameObject.Find("Player").GetComponent<Animator>();

        //tallennan alkuperaisen cooldownin
        CoolDown = attackCooldown;
        attackCooldown = 0;
        theEnemy = GameObject.FindWithTag("Enemy");

        enemyBlock = theEnemy.transform.GetChild(1).GetComponentInChildren<ParticleSystem>();
        enemyBlood = theEnemy.transform.GetChild(2).GetComponentInChildren<ParticleSystem>();

        //TakeDmg takeDmg = thePlayer.GetComponent<TakeDmg>();

    }

  

    public void _attack()
    {

        if(attackCooldown <= 0 && !block)
        {

            mAnimator.SetTrigger("Punch");
            attackCooldown = CoolDown;        //resettaa cooldownin
            
            //tarkistaa ettei vastustaja suojaa
            if(AIScript.AiDefence == false && !block)
            {
               
                enemyBlood.Play();
                Debug.Log("Hit");
               
                    GameObject.FindWithTag("Enemy").GetComponent<TakeDmg>().currentHealth -= Damage;
               
            }
            else if(AIScript.AiDefence == true)
            {
                enemyBlock.Play(); //lyonti blokattiin
                                   /*_hpBar.GetComponent<HealthbarScript>().hp -= BlockedDamage;*/ //tekee vahan dmg jos lyonti blokataan
                //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.right);

                //if (hit.collider.tag == "Enemy")
                //{
                    GameObject.FindWithTag("Enemy").GetComponent<TakeDmg>().currentHealth -= BlockedDamage;
                //}
            }

           
        }

    }

   
    //Defence nappiin tarkistus onko se pohjassa vai ei
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
        if (block)
        {
            block = true;
        }
        else
        {
            block = false;
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

       
        attackCooldown -= Time.deltaTime;

    }
}
