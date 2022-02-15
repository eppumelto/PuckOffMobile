using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{


    //attack systeemi
    public int dmg;
    public int blockDmg;

    //Boss hp
    public int BossHp;

    //Cooldown
    public float cooldown;
    private float _oriCooldown;

    //particle effects
    public ParticleSystem blood;
    public ParticleSystem BlockParticle;

    private Animator bAnimator;

    void Start()
    {
        GameObject thePlayer = GameObject.Find("Pelaaja");
        _oriCooldown = cooldown; //otetaan alkuperainen cooldown
        //gameObject.GetComponent<TakeDmg>().maxHealth = BossHp; //asetetaan bossin hp
        bAnimator = GameObject.FindWithTag("Enemy").GetComponent<Animator>();
        BlockParticle = thePlayer.transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
        blood = thePlayer.transform.GetChild(1).GetComponentInChildren<ParticleSystem>();
    }

    public void BossAttack()
    {
        bAnimator.SetTrigger("Attack");
        //Tekee damagen pelaajaan sen perusteella suojasiko pelaaja iskun vai ei
        if(FightScript.block == true)
        {
            GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= blockDmg;   //pelaaja suojasi iskun
            BlockParticle.Play();
        }
        else
        {
            GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= dmg;   //Pelaaja ei suojannut iskua
            blood.Play();
        }

        cooldown = _oriCooldown;    //resettaa cooldownin
    }




  
    void Update()
    {
        //Boss lyo
        if (cooldown <= 0 && TakeDmg.PlayerAlive)
        {
            
            BossAttack();

        }

        cooldown -= Time.deltaTime;

    }
}
