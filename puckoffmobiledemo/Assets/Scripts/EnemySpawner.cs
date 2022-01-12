using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    ObjectPooling objectPooler;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooling.Instance;
    }

    private void FixedUpdate()
    {
        objectPooler.SpawnFromPool("Enemy", transform.position, Quaternion.identity);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
