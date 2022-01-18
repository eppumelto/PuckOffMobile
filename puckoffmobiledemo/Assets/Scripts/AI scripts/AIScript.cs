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

    private bool playerDef;                    //vastustajan block
    public static bool AiDefence;             //AI defence
    private float AiDefTime;                 //kertoo kauan AI suojaa
    
    public ParticleSystem blood;           //Pelaajan veri
    public ParticleSystem blockParticle;  //pelaajan blockp partikkeli

    public int healtToPlayer;           //kun ai kuolee antaa hp:ta pelaajalle
/*    public GameObject PlayerHealtbar;*/  //Pelaajan healtbar


    //botin cooldown toimii sen perusteella suojaako se vai ei, jos se suojaa cooldown kestaa yhta kauan kun suojaus muuten se kestaa original cooldownin verran

    private void Start()
    {
        _originalCoolDown = CoolDown;
        GameObject thePlayer = GameObject.Find("Pelaaja");

        blockParticle = thePlayer.transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
        blood = thePlayer.transform.GetChild(1).GetComponentInChildren<ParticleSystem>();
    }

    public void agressive()
    {
        //isompi mahis lyoda
      int rnd = Random.Range(0, 10);

        if(rnd >= 3)
        {
            //lyo
            if (!playerDef)
            {
                //PlayerHealtbar.GetComponent<HealthbarScript>().hp -= dmg;
                GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= dmg;
                blood.Play();
                
   
            }
            else if (playerDef)
            {
                blockParticle.Play(); //Lyonti suojattiin
               
                    GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= blockedDmg;
            }
            //defendaa
        }
        else if(rnd <= 2)
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

            if (!playerDef)
            {
   
                    GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= dmg;

      
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
            
            if (!playerDef)
            {
                GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= dmg;
                blood.Play();
             
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

        if(AiDefTime > 0)
        {
            AiDefence = true;
            AiDefTime -= Time.deltaTime;
        }
        else
        {
            AiDefence = false;
        }


        playerDef = FightScript.block;

        healt = GameObject.FindWithTag("Enemy").GetComponent<TakeDmg>().currentHealth;
        //AI tappelee oman healtin mukaan
        if (healt >= 70 && CoolDown <= 0 && !AiDefence)
        {
            agressive();

        }
        else if(healt < 70 && healt > 30 && CoolDown <= 0 && !AiDefence)
        {
            normal();
        }
        else if(healt <= 30 && CoolDown <= 0 && !AiDefence)
        {
            Defencive();
        }
        CoolDown -= Time.deltaTime;


      

 

    }
}
