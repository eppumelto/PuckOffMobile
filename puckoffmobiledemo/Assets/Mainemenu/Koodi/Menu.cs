using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject pauseButton;

    public GameObject CitySelectPanel;
    public GameObject MainMenuPanel;

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseButton.SetActive(true);
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
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
    public void MenuButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    public void Quit()
    {
        Application.Quit();
    }
}