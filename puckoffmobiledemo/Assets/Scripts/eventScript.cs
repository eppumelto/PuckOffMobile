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
<<<<<<< HEAD
=======
        SingleLevel.instance.levelNum++;
        PlayerPrefs.SetInt("Lv" + SingleLevel.instance.levelIndex, SingleLevel.instance.levelNum);
>>>>>>> 79062d34ad8ba9cb0060584f0f58096b1ba732fb
        Debug.Log("Voitto");
       //voi lisata seuraavan avatun tason ja tallettaa sen

    }
 


    void Start()
    {
       // Debug.Log(SingleLevel.instance.levelNum);
        deathPanel.SetActive(false);
        VictoryPanel.SetActive(false);
       
        PlayerAnimator = GameObject.Find("Player").GetComponent<Animator>();
       
    }


  
}
