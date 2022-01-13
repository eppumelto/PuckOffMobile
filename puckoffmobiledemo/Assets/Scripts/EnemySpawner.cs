using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnCooldown;
    public  float cooldown;

    ObjectPooling objectPooler;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooling.Instance;
        cooldown = Time.time + spawnCooldown;
    }

    private void FixedUpdate()
    {
        if (TakeDmg.isAlive == false && cooldown < Time.time)
        {
            objectPooler.SpawnFromPool("Enemy", transform.position, Quaternion.identity);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
