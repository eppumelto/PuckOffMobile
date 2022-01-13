using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIScript : MonoBehaviour
{
    public float CoolDown; //cooldown jota kaytetaan
    public int dmg;       //paljon ai tekee dmg lyonnilla
    public int blockedDmg; //paljon ai tekee dmg jos pelaaja blokkaa lyonnin
    private float _originalCoolDown; //otetaan alkuperainen cooldown talteen
    public static float healt;      //kertoo paljon AI lla on hp
    public static HealthbarScript HealtScript;

    
    public GameObject PlayerHealtbar; //Pelaajan healtbar
    public ParticleSystem blood;     //Pelaajan veri
    public ParticleSystem blockParticle;

    private FightScript FightScript;
    private bool playerDef; //vastustajan block
    public static bool AiDefence; //AI defence
    private float AiDefTime;     //kertoo kauan AI suojaa
   
    
  //botin cooldown toimii sen perusteella suojaako se vai ei, jos se suojaa cooldown kestaa yhta kauan kun suojaus muuten se kestaa original cooldownin verran
    
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
                //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.left);
                //if (hit.collider.tag == "Player")
                //{
                    GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= dmg;

                //}
                //PlayerHealtbar.GetComponent<HealthbarScript>().hp -= dmg;
                blood.Play();
            }
            else if (playerDef)
            {
                blockParticle.Play(); //Lyonti suojattiin
                                      //PlayerHealtbar.GetComponent<HealthbarScript>().hp -= blockedDmg;
                //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.left);
                //if (hit.collider.tag == "Player")
                //{
                    GameObject.Find("Pelaaja").GetComponent<TakeDmg>().currentHealth -= blockedDmg;
                //}
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







    private void Start()
    {
        _originalCoolDown = CoolDown;
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

        healt = GameObject.Find("vihu").GetComponent<TakeDmg>().currentHealth;
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

        if(healt <= 0)
        {
            Destroy(this.gameObject);
        }

 

    }
}
