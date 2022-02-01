using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eventScript : MonoBehaviour
{
    //Scriptit
    private TakeDmg _takedmg;

    //PlayerDead
    public GameObject deathPanel;
    private Animator PlayerAnimator;

    //PlayerWON
    public GameObject VictoryPanel;

    public void PlayerDead()
    {
        //_takedmg.headSprites[0];          Toho vaihtaa pelaajan naaman siihe mis on silmat kiinni
        deathPanel.SetActive(true);
        Time.timeScale = 0.5f;      //slowmotion JIIIHIII
        PlayerAnimator.SetTrigger("Die");
        
    }


    //pelaaja voitti tason
    public void PlayerWON()
    {
        VictoryPanel.SetActive(true);
       //voi lisata seuraavan avatun tason ja tallettaa sen
    }
 


    void Start()
    {
        deathPanel.SetActive(false);
        VictoryPanel.SetActive(false);
       
        PlayerAnimator = GameObject.Find("Player").GetComponent<Animator>();
       
    }


  
}
