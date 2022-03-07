using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UiManager : MonoBehaviour
{
    private BussiKoodi _bussikoodi;
    private GameObject ukkeli;

    public static UiManager instance;

    public int levels;
    public GameObject mapSelectionPanel;
    public GameObject[] levelSelectionPanels;


    public MapScript[] mapSelections;


    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (instance != this)
            {
                //Destroy(gameObject);
            }
        }
        
    }
    private void Start()
    {
        //PlayerPrefs.SetInt("Lv ", 1);
        levels = PlayerPrefs.GetInt("Lv");
        
        //PlayerPrefs.DeleteAll();
    }

    
    public void PressMapButton(int _mapIndex)       //nappia on painettu
    {
   
       if(mapSelections[_mapIndex].isUnlocked == true) //jos kenttä on auki
        {
            ukkeli = GameObject.FindWithTag("ukkeli");
            if (ukkeli.transform.position == mapSelections[_mapIndex].gameObject.GetComponent<Button>().transform.position)
            {
                levelSelectionPanels[_mapIndex].gameObject.SetActive(true);
                mapSelectionPanel.gameObject.SetActive(false);
            }
            

            ukkeli.transform.position = mapSelections[_mapIndex].GetComponent<Button>().transform.position;

          

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
