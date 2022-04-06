using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoStop : MonoBehaviour
{
    public VideoPlayer VideoPlayer;
    public bool isPlayerStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isPlayerStarted == false && VideoPlayer.isPlaying == true)
        {
            // When the player is started, set this information
            isPlayerStarted = true;
            FindObjectOfType<AudioManager>().Stop("Theme");
        }
        if (isPlayerStarted == true && VideoPlayer.isPlaying == false)
        {
            // Wehen the player stopped playing, hide it
            VideoPlayer.gameObject.SetActive(false);
            
        }

    }
}
