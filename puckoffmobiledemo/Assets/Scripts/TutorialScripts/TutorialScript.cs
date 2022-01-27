using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    

    public Text[] _txt;      //teksti jolla kertoo asioita
    public Image[] _nuolet; //nuolet jolla osoitetaan asioita
    private int tutorialStage = 1;

    void Start()
    {

        //for(int i = 0; i < _txt.Length; i++)
        //{
        //    _txt[i].enabled = false;
        //}

        _txt[0].enabled = true;
        _nuolet[0].enabled = true;
        Time.timeScale = 0;
       
    }



    private void Update()
    {
        //PAINA SUOJAUSNAPPIA
        if (FightScript.block == true)
        {
            _txt[0].enabled = false;
            _nuolet[0].enabled = false;
            tutorialStage++;
        }
            
    

    }


    }



