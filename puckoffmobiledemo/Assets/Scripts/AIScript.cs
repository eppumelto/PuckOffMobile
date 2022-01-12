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
   
    
  
    
    public void agressive()
    {
        //isompi mahis lyoda
      int rnd = Random.Range(0, 10);

        if(rnd >= 3)
        {
            
            if (!playerDef)
            {
                PlayerHealtbar.GetComponent<HealthbarScript>().hp -= dmg;
                
                blood.Play();
                
                Debug.Log("Attack agressive" + rnd);
            }
            else if (playerDef)
            {
                blockParticle.Play(); //Lyonti suojattiin
                PlayerHealtbar.GetComponent<HealthbarScript>().hp -= blockedDmg;
            }

        }
        else if(rnd <= 2)
        {
            Debug.Log("Defence " + rnd);
            AiDefTime = 3; // aloittaa suojauksen
            
        }
        CoolDown = _originalCoolDown; //resettaa cooldownin
    }

    public void Defencive()
    {
        //Isompi mahis suojata
       int rnd = Random.Range(0, 10);

        if (rnd >= 7)
        {
            Debug.Log("Attack " + rnd);
            if (!playerDef)
            {
                PlayerHealtbar.GetComponent<HealthbarScript>().hp -= dmg;
                blood.Play();
            }
            else if (playerDef)
            {
                blockParticle.Play(); //Lyonti suojattiin
                PlayerHealtbar.GetComponent<HealthbarScript>().hp -= blockedDmg;
            }

        }
        else if (rnd <= 6)
        {
            Debug.Log("Defence " + rnd);
            AiDefTime = 3; // aloittaa suojauksen

        }
        CoolDown = _originalCoolDown;

    }

    public void normal()
    {
        //tekee molempia yhta paljon
       int rnd = Random.Range(0, 10);

        if (rnd >= 5)
        {
            
            if (!playerDef)
            {
                PlayerHealtbar.GetComponent<HealthbarScript>().hp -= dmg;
                blood.Play();
                Debug.Log("Attack " + rnd);
            }
            else if (playerDef)
            {
                blockParticle.Play(); //Lyonti suojattiin
                PlayerHealtbar.GetComponent<HealthbarScript>().hp -= blockedDmg;
            }

        }
        else if (rnd <= 4)
        {
            Debug.Log("Defence " + rnd);
            AiDefTime = 3; // aloittaa suojauksen

        }
        CoolDown = _originalCoolDown;

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
        healt = TakeDmg.currentHealth;
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
