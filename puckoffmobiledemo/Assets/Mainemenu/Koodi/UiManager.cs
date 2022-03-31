using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UiManager : MonoBehaviour
{
    private GameObject ukkeli;

    public static UiManager instance;
    public Button playBtn;
    public GameObject playObject; //playbuttonin gameobject (juu oisin voinnu teha sen eri taval :))
    private int levelSelected;

    public int levels;
    public GameObject mapSelectionPanel;
    public GameObject[] levelSelectionPanels;
   

    public MapScript[] mapSelections;

    public Animator transitionAnim;

    public Image image;

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
        playBtn.enabled = false;
        playObject.SetActive(false);
        //PlayerPrefs.DeleteAll();

      

    }

    
    public void PressMapButton(int _mapIndex)       //nappia on painettu
    {

        levelSelected = _mapIndex;

       if(mapSelections[_mapIndex].isUnlocked == true) //jos kenttä on auki
        {
            ukkeli = GameObject.FindWithTag("ukkeli");
            //if (ukkeli.transform.position == mapSelections[_mapIndex].gameObject.GetComponent<Button>().transform.position)
            //{
            //    levelSelectionPanels[_mapIndex].gameObject.SetActive(true);
            //    mapSelectionPanel.gameObject.SetActive(false);
            //}
            

            ukkeli.transform.position = mapSelections[_mapIndex].GetComponent<Button>().transform.position;
            playBtn.enabled = true;
            playObject.SetActive(true);

       }
       else
       {
            playObject.SetActive(false);
            Debug.Log("This map is not available");
       }
    }


   public void pressPlayBtn()
    {
            levelSelectionPanels[levelSelected].gameObject.SetActive(true);
            mapSelectionPanel.gameObject.SetActive(false);
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
        StartCoroutine(SceneLoad(sceneName));

    }

    IEnumerator SceneLoad(string sceneName)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
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
