using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private GameObject obj;
    public GameObject[] RandomVihut;
    private bool SpawnNormal = true;


        [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPooling Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    public List<Pool> pools;
    

    public Dictionary<string, Queue<GameObject>> PoolDictionary;
    void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            
            for (int i = 0; i < pool.size; i++)
            {

                if(pool.size != 1 - i && SpawnNormal)
                {
                    //Luo vastustajan randomisti
                    GameObject obj = Instantiate(RandomVihut[Random.Range(0,RandomVihut.Length)]);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }
                else
                {
                    SpawnNormal = false;
                    
                }

               

           
                //katotaan viiminen vihu
                if (i + 1 == pool.size && pools.Count == 1 && !SpawnNormal)
                {
                    //luo viimeisen vastustajan
                    GameObject obj = Instantiate(RandomVihut[Random.Range(0, RandomVihut.Length)]);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);

                    obj.GetComponent<TakeDmg>().isLast = true;

                }
                else if(i + 1 == pool.size && pools.Count == 2 && !SpawnNormal)
                {
                    //Luo bossin
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);

                    obj.GetComponent<TakeDmg>().isLast = true;

                }

            }

            PoolDictionary.Add(pool.tag, objectPool);

        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {   
        if (!PoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + " does not exist");
            return null;
        }

       GameObject objectToSpawn = PoolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        PoolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

 
}
