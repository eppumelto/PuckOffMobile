using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        if (SceneManager.GetActiveScene().buildIndex == UiManager.instance.levels)
        {
            UiManager.instance.levels++;
            PlayerPrefs.SetInt("Lv",  UiManager.instance.levels);

        }

        Won = true;
        Debug.Log("Voitto");
       //voi lisata seuraavan avatun tason ja tallettaa sen

    }
 


    void Start()
    {
        deathPanel.SetActive(false);
        VictoryPanel.SetActive(false);
       
        PlayerAnimator = GameObject.Find("Player").GetComponent<Animator>();
       
    }


  
}
