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

    //public List<GameObject> listOfEnemies = new List<GameObject>();


    public void PlayerDead()
    {
        //_takedmg.headSprites[0];          Toho vaihtaa pelaajan naaman siihe mis on silmat kiinni
        deathPanel.SetActive(true);
        Time.timeScale = 0.5f;      //slowmotion JIIIHIII
        PlayerAnimator.SetTrigger("Die");
        
    }


 




   // }

    //public void EnemyDead(GameObject enemy)
    //{
    //    if (listOfEnemies.Contains(enemy))
    //    {
    //        listOfEnemies.Remove(enemy);
    //    }
    //}


    void Start()
    {
        deathPanel.SetActive(false);
        Time.timeScale = 1;
        PlayerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        //listOfEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }


  
}
