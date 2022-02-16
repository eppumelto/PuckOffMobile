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

    public AudioSource BossSpawn;

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

        else if ( TakeDmg.enemiesKilled == 2 && TakeDmg.isAlive == false && TakeDmg.PlayerAlive && objectPooler.pools.Count == 2)
        {
            BossTxt.SetActive(true);
            objectPooler.SpawnFromPool("Boss",new Vector3(transform.position.x,transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
            BossSpawn.Play();
            
            
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
