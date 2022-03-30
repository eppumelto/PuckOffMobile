using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    //koodit joiden kanssa toimii
    private TakeDmg takeDmg;
    ObjectPooling objectPooler;

    public GameObject enemy;                //vihu
    public float spawnCooldown = 2;        //Spawncooldown
    

  

    //Boss juttuja
    public GameObject BossTxt;          //Boss text joka tulee kun boss spawnataan
    public AudioSource BossSpawn;      //Audio joka laitetaan kun boss tulee


    void Start()
    {
            //otetaan koodit
        takeDmg = GameObject.Find("Pelaaja").GetComponent<TakeDmg>();
        objectPooler = ObjectPooling.Instance;
      
    }

    private void FixedUpdate()
    {
        //jos vihu on kuollut ja spawncooldown on 0 ja pelaaja on hengissä
        if (TakeDmg.isAlive == false && spawnCooldown <= 0 && TakeDmg.PlayerAlive)
        {
            objectPooler.SpawnFromPool("Enemy", transform.position, Quaternion.identity);
            spawnCooldown = 0;
        }
        //jos vihuja tapettu ainakin 1 niin spawnaa bossin
        else if ( TakeDmg.enemiesKilled == 2 && TakeDmg.isAlive == false && TakeDmg.PlayerAlive && objectPooler.pools.Count == 2)
        {
            BossTxt.SetActive(true);
            objectPooler.SpawnFromPool("Boss",new Vector3(transform.position.x,transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
            BossSpawn.Play();
            
        }

        
    }



    void Update()
    {
        //cooldown miinustuu
        if (TakeDmg.isAlive == false)
        {
            spawnCooldown -= Time.deltaTime;
        }
    }
}
