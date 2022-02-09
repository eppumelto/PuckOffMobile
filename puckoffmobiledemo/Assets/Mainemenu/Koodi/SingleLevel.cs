using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLevel : MonoBehaviour
{
    public static SingleLevel instance;

    public int levelNum = 0;
    public int levelIndex;

    //void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else if (instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    public void PressStartButton(int _levelNum) //Tämä metodi triggeröityy kun painat mitä tahansa levle nappia 
    {
        levelNum = _levelNum;

        // Vain sinun levelien numero on isompi kuin tallentajan, voit tallentaa uuden recordin
        // PlayerPrefs.Getint("Lv" + levelIndex) default value 0
        if (levelNum > PlayerPrefs.GetInt("Lv" + levelIndex) && eventScript.Won == true) //KEY: Lv1; value Level Number
        {
            PlayerPrefs.SetInt("Lv" + levelIndex, levelNum);
        }
        Debug.Log("Saving data is " + PlayerPrefs.GetInt("Lv " + levelIndex));
        UiManager.instance.BackMapSelection();
    }
}
