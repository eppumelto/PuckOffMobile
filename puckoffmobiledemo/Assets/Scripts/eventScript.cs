using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eventScript : MonoBehaviour
{
    //PlayerDead
    public GameObject deathPanel;



    public void PlayerDead()
    {

        deathPanel.SetActive(true);
        Time.timeScale = 0.5f;      //slowmotion JIIIHIII

    }


    void Start()
    {
        deathPanel.SetActive(false);
        Time.timeScale = 1;
    }


    void Update()
    {
        
    }
}
