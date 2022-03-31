using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{   

    public Sound[] sounds;
    private AudioSource fightMusic;
    private bool inMainMenu = true;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }

        

    }

    void Start()
    {

        

    }

    private void Update()
    {

        if (SceneManager.GetActiveScene().name != "MainMenu" && inMainMenu)
        {
            Play("Theme");
            Debug.Log("Moi1");
            inMainMenu = false;
        }
        else if(SceneManager.GetActiveScene().name == "MainMenu" && !inMainMenu)
        {
            Stop("Theme");
            Debug.Log("Moi2");
            inMainMenu = true;
        }


    }

  
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }


}
