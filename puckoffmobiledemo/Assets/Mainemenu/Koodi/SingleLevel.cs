using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLevel : MonoBehaviour
{
    private int levelNum = 0;
    public int levelIndex;

    public void PressStartButton(int _levelNum) //T�m� metodi trigger�ityy kun painat mit� tahansa levle nappia 
    {
        levelNum = _levelNum;

        // Vain sinun levelien numero on isompi kuin tallentajan, voit tallentaa uuden recordin
        // PlayerPrefs.Getint("Lv" + levelIndex) default value 0
        if(levelNum > PlayerPrefs.GetInt("Lv" + levelIndex)) //KEY: Lv1; value Level Number
        {
            PlayerPrefs.SetInt("Lv" + levelIndex, levelNum);
        }
        Debug.Log("Saving data is " + PlayerPrefs.GetInt("Lv " + levelIndex));
        UiManager.instance.BackMapSelection();
    }
}
