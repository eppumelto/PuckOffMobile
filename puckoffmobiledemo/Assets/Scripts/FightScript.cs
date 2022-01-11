using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightScript : MonoBehaviour
{
    //attack
    public float attackCooldown;
    private float CoolDown;

    //defence
    private bool block;

    public void _attack()
    {

        if(attackCooldown <= 0)
        {
            attackCooldown = CoolDown;
            Debug.Log("Attack");
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
        //Tarkistaa suojaako pelaaja
        if (block)
        {
            Debug.Log("Defence Active");
        }

        
        attackCooldown -= Time.deltaTime;

    }
}
