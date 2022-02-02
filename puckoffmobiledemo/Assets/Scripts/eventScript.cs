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
    public static bool Won = false;//kaytetaan SingleLevel Scriptissa

    public void PlayerDead()
    {
        //_takedmg.headSprites[0];          Toho vaihtaa pelaajan naaman siihe mis on silmat kiinni
        deathPanel.SetActive(true);   
        PlayerAnimator.SetTrigger("Die");
        
    }


    //pelaaja voitti tason
    public void PlayerWON()
    {
        VictoryPanel.SetActive(true);
<<<<<<< HEAD
        Won = true;
        SingleLevel.instance.levelNum++;
        PlayerPrefs.SetInt("Lv" + SingleLevel.instance.levelIndex, SingleLevel.instance.levelNum);
=======
       //voi lisata seuraavan avatun tason ja tallettaa sen
>>>>>>> 81cd37a1461bc3c1ba2d9b62660277de617f4c08
    }
 


    void Start()
    {
        deathPanel.SetActive(false);
        VictoryPanel.SetActive(false);
       
        PlayerAnimator = GameObject.Find("Player").GetComponent<Animator>();
       
    }


  
}
