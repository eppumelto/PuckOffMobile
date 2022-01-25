using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public bool isUnlocked = false;
    public GameObject locked;
    public GameObject unlocked;

    public int mapIndex;//the index of this map
    public int levelNum;//how many level until map unlock
    public int startLevel; 
    public int endLevel;
    private void Update()
    {
        UpdateMap();
        UnlockMap();
    }
    private void UpdateMap()
    {
        if(isUnlocked)//uuga buuga this map is good
        {
            unlocked.gameObject.SetActive(true);
            locked.gameObject.SetActive(false);
        }
        else//This map locked uuga buuga, need more clear
        {
            unlocked.gameObject.SetActive(false);
            locked.gameObject.SetActive(true);
        }
    }
    private void UnlockMap()
    {
        if (FindObjectOfType<UiManager>().levels > levelNum)
            isUnlocked = true;
        else
            isUnlocked = false;

    }
}
