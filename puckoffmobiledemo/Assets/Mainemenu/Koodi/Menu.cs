using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;

    public GameObject CitySelectPanel;
    public GameObject MainMenuPanel;
    private GameObject instance;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this.gameObject;
    //    }
    //    else
    //    {
    //        if (instance != this)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //    DontDestroyOnLoad(gameObject);

    //}


    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pauseButton.SetActive(false);

    }
    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Play()
    {
        CitySelectPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }
    public void backToMenu()
    {
        CitySelectPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
