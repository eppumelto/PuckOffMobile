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
    public GameObject _hpBar;

    //defence
    public static bool block;

    public void _attack()
    {

        if(attackCooldown <= 0 && !block)
        {
           
            
            attackCooldown = CoolDown;        //resettaa cooldownin
           
            //tarkistaa ettei vastustaja suojaa
            if(AIScript.AiDefence == false)
            {
                TakeDmg.currentHealth -= Damage; //tekee dmg vastustajaan
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
