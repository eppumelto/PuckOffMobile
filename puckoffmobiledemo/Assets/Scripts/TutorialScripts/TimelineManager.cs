using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public FightScript _fightscript;
    public ObjectPooling objectPooler;
    public float timeToStart;

    private void Update()
    {
        
        if(timeToStart > 0)
        {
            timeToStart -= Time.deltaTime;
        }
        else
        {
            objectPooler.SpawnFromPool("Enemy", transform.position, Quaternion.identity);
            _fightscript.enabled = true;
            Destroy(this);
        }

    }

}
