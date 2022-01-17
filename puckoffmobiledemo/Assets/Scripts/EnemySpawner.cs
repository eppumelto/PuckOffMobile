using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnCooldown = 2;
    private  float cooldown;
    private TakeDmg takeDmg;

    ObjectPooling objectPooler;

    // Start is called before the first frame update
    void Start()
    {
        takeDmg = GameObject.Find("Pelaaja").GetComponent<TakeDmg>();
        objectPooler = ObjectPooling.Instance;

      
    }

    private void FixedUpdate()
    {
        if (TakeDmg.isAlive == false && spawnCooldown <= 0)
        {
            objectPooler.SpawnFromPool("Enemy", transform.position, Quaternion.identity);
            spawnCooldown = 2;
        }

        else if ( TakeDmg.enem&& TakeDmg.isAlive == false)
        {
            objectPooler.SpawnFromPool("Boss", transform.position, Quaternion.identity);
        }

        
    }



    // Update is called once per frame
    void Update()
    {

        if (TakeDmg.isAlive == false)
        {
            spawnCooldown -= Time.deltaTime;
        }
    }
}
