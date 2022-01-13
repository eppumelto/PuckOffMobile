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

    void Start()
    {

        //tallennan alkuperaisen cooldownin
        CoolDown = attackCooldown;
        attackCooldown = 0;

        //GameObject thePlayer = GameObject.Find("Pelaaja");
        //TakeDmg takeDmg = thePlayer.GetComponent<TakeDmg>();

    }

  

    public void _attack()
    {

        if(attackCooldown <= 0 && !block)
        {
           
            
            attackCooldown = CoolDown;        //resettaa cooldownin
            
            //tarkistaa ettei vastustaja suojaa
            if(AIScript.AiDefence == false && !block)
            {
               
                enemyBlood.Play();
                Debug.Log("Hit");
               
                    GameObject.Find("vihu").GetComponent<TakeDmg>().currentHealth -= Damage;
               
            }
            else if(AIScript.AiDefence == true)
            {
                enemyBlock.Play(); //lyonti blokattiin
                                   /*_hpBar.GetComponent<HealthbarScript>().hp -= BlockedDamage;*/ //tekee vahan dmg jos lyonti blokataan
                //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.right);

                //if (hit.collider.tag == "Enemy")
                //{
                    GameObject.Find("vihu").GetComponent<TakeDmg>().currentHealth -= BlockedDamage;
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
        //Tarkistaa suojaako pelaaja ja jos pelaaja lyo niin ei voi suojata heti samaan aikaan
        if (block)
        {
            block = true;
        }
        else
        {
            block = false;
        }

       
        attackCooldown -= Time.deltaTime;

    }
}
