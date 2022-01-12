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
    public GameObject _hpBar;
    public ParticleSystem enemyBlood;

    //defence
    public static bool block;
    public ParticleSystem enemyBlock;

    public void _attack()
    {

        if(attackCooldown <= 0 && !block)
        {
           
            
            attackCooldown = CoolDown;        //resettaa cooldownin
            
            //tarkistaa ettei vastustaja suojaa
            if(AIScript.AiDefence == false)
            {
                //TakeDmg.currentHealth -= Damage; //tekee dmg vastustajaan
                _hpBar.GetComponent<HealthbarScript>().hp -= Damage;
                enemyBlood.Play();
                Debug.Log("Hit");
            }
            else if(AIScript.AiDefence == true)
            {
                enemyBlock.Play(); //lyonti blokattiin
                _hpBar.GetComponent<HealthbarScript>().hp -= BlockedDamage; //tekee vahan dmg jos lyonti blokataan
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


    void Start()
    {
        
        //tallennan alkuperaisen cooldownin
        CoolDown = attackCooldown;
        attackCooldown = 0;

        

    }

   
    void Update()
    {
        //Tarkistaa suojaako pelaaja ja jos pelaaja lyo niin ei voi suojata heti samaan aikaan
        if (block && attackCooldown <= 0)
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
