using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FightScript : MonoBehaviour
{
    

    //attack
    public float attackCooldown;
    private float CoolDown;
    public int Damage = 100;
    public GameObject _hpBar;
    public GameObject Character;

    //defence
    private bool block;

    public void _attack()
    {

        if(attackCooldown <= 0 && !block)
        {
            attackCooldown = CoolDown;
            //_hpBar.GetComponent<HealthbarScript>().currentHealth -= Damage;
            Debug.Log("Attack");

            TakeDmg.currentHealth -= Damage;
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
            Debug.Log("Defence Active");
        }
        else
        {
            block = false;
           
        }

        
        attackCooldown -= Time.deltaTime;

    }
}
