using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CameraShake : MonoBehaviour
{

    public float ShakeDuration = 0.3f;          // Efektin kesto
    public float ShakeAmplitude = 1.2f;         // Cinemachine Noise Profile Parameter
    public float ShakeFrequency = 2.0f;         // Cinemachine Noise Profile Parameter

    private float ShakeElapsedTime = 0f;

    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    // Use this for initialization
    void Start()
    {
        // Etsit‰‰n virtuaalinen kamera
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }


    void Update()
    {
        // Jos cinemachinea ei ole, koodi ei updatee
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // Jos efekti on p‰‰ll‰
            if (ShakeElapsedTime > 0)
            {
                // Laitetaan parametrit
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // P‰ivitet‰‰n timeri
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // Jos efekti loppuu, resetoidaan
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }

    }

    public void Effect1()
    {

        ShakeElapsedTime = ShakeDuration;


        
    }
}