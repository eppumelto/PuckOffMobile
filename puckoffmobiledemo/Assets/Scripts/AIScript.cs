using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIScript : MonoBehaviour
{
    public float CoolDown;
    public int dmg;
    private float _originalCoolDown;
    private float healt;
    private int rnd;
    public static HealthbarScript HealtScript;
    public GameObject PlayerHealtbar;
   
    
    private FightScript FightScript;
    private bool playerDef; //vastustajan block
    public static bool AiDefence; //AI defence
    private float AiDefTime;     //kertoo kauan AI suojaa
    public void agressive()
    {
        //isompi mahis lyoda
       rnd = Random.Range(0, 10);

        if(rnd >= 4)
        {
            Debug.Log("Attack " + rnd);
            if (!playerDef)
            {
                PlayerHealtbar.GetComponent<HealthbarScript>().hp -= dmg;
            }

        }
        else if(rnd <= 3)
        {
            Debug.Log("Defence " + rnd);
            AiDefTime = 5; // aloittaa suojauksen
            
        }
        CoolDown = _originalCoolDown;
    }

    public void Defencive()
    {
        //Isompi mahis suojata
        rnd = Random.Range(0, 10);

        if (rnd >= 7)
        {
            Debug.Log("Attack " + rnd);
            if (!playerDef)
            {
                PlayerHealtbar.GetComponent<HealthbarScript>().hp -= dmg;
            }

        }
        else if (rnd <= 6)
        {
            Debug.Log("Defence " + rnd);
            AiDefTime = 5; // aloittaa suojauksen

        }
        CoolDown = _originalCoolDown;

    }

    public void normal()
    {
        //tekee molempia yhta paljon
        rnd = Random.Range(0, 10);

        if (rnd >= 5)
        {
            Debug.Log("Attack " + rnd);
            if (!playerDef)
            {
                PlayerHealtbar.GetComponent<HealthbarScript>().hp -= dmg;
            }

        }
        else if (rnd <= 4)
        {
            Debug.Log("Defence " + rnd);
            AiDefTime = 5; // aloittaa suojauksen

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

    }
}
