using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnCooldown = 2;
    private  float cooldown;
    private TakeDmg takeDmg;

    ObjectPooling objectPooler;

    //Boss juttuja
    public GameObject BossTxt;

    void Start()
    {
        takeDmg = GameObject.Find("Pelaaja").GetComponent<TakeDmg>();
        objectPooler = ObjectPooling.Instance;
      
    }

    private void FixedUpdate()
    {
        if (TakeDmg.isAlive == false && spawnCooldown <= 0 && TakeDmg.PlayerAlive)
        {
            objectPooler.SpawnFromPool("Enemy", transform.position, Quaternion.identity);
            spawnCooldown = 2;
        }

        else if ( TakeDmg.enemiesKilled == 5 && TakeDmg.isAlive == false && TakeDmg.PlayerAlive)
        {
            BossTxt.SetActive(true);
            
            objectPooler.SpawnFromPool("Boss", transform.position, Quaternion.identity);
        }

        
    }



    void Update()
    {

        if (TakeDmg.isAlive == false)
        {
            spawnCooldown -= Time.deltaTime;
        }
    }
}
