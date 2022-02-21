using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    //Scriptiä käytetään vain Tutorial scenessä

    //koodit joita tarvitaan
    public FightScript _fightscript;        
    public ObjectPooling objectPooler;
    public TakeDmg takedmg;
    public EnemySpawner spawner;

    //kertoo koska peli alkaa
    public float timeToStart;

    private void Update()
    {
        //katsotaan onko peli alkanut jos ei niin otetaan aikaa niin kauan kunnes peli alkaa
        if(timeToStart > 0)
        {
            timeToStart -= Time.deltaTime;
        }
        else
        {
            //peli alkoi luodaan hahmo ja laitetaan koodit toimimaan lopuksi poistetaan timelinemanager koodi
            objectPooler.SpawnFromPool("Enemy", transform.position, Quaternion.identity);
            Debug.Log("Moi");
            _fightscript.enabled = true;
            takedmg.enabled = true;
            spawner.enabled = true;
            Destroy(this);
        }

    }

}
