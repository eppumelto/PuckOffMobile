using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eventScript : MonoBehaviour
{
    //PlayerDead
    public GameObject deathPanel;

    public List<GameObject> listOfEnemies = new List<GameObject>();


    public void PlayerDead()
    {

        deathPanel.SetActive(true);
        Time.timeScale = 0.5f;      //slowmotion JIIIHIII

    }

    public void EnemyDead(GameObject enemy)
    {
        if (listOfEnemies.Contains(enemy))
        {
            listOfEnemies.Remove(enemy);
        }
    }


    void Start()
    {
        deathPanel.SetActive(false);
        Time.timeScale = 1;

        listOfEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }


    void Update()
    {
        
    }
}
