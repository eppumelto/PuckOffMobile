using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public int levels;
    public GameObject mapSelectionPanel;
    public GameObject[] levelSelectionPanels;

    public MapScript[] mapSelections;


    //private void Awake()
    //{
    //    if(instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        if(instance != this)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //    DontDestroyOnLoad(gameObject);
            
    //}
    private void Start()
    {
        //PlayerPrefs.SetInt("Lv ", 1);
        levels = PlayerPrefs.GetInt("Lv");
        //PlayerPrefs.DeleteAll();
    }
    private void Update()
    {

    }
    public void PressMapButton(int _mapIndex)
    {
       if(mapSelections[_mapIndex].isUnlocked == true)
       {
            levelSelectionPanels[_mapIndex].gameObject.SetActive(true);
            mapSelectionPanel.gameObject.SetActive(false);
       }
       else
       {
            Debug.Log("This map is not available");
       }
    }
    public void BackButton()
    {
        mapSelectionPanel.gameObject.SetActive(true);
        for (int i = 0; i < mapSelections.Length; i++)
        {
            levelSelectionPanels[i].gameObject.SetActive(false);
        }
    }
    public void SceneTransition(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }
    public void BackMapSelection()
    {
        mapSelectionPanel.gameObject.SetActive(true);
        for (int i = 0; i < mapSelections.Length; i++)
        {
            levelSelectionPanels[i].gameObject.SetActive(false);
        }
        SceneManager.LoadScene("MainMenu");
    }
}
