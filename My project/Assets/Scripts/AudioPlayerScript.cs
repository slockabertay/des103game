using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource levelAudioSource;
    [SerializeField] AudioClip levelMusic;
    [SerializeField] AudioClip pausedMusic;

    void Start()
    {
        levelAudioSource = GetComponent<AudioSource>();
        levelAudioSource.clip = levelMusic;
        levelAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.paused || PlayerController.gameOverScreen)
        {
            if (levelAudioSource.clip != pausedMusic )
            {
                float playTime = levelAudioSource.time;
                levelAudioSource.Stop();
                levelAudioSource.clip = pausedMusic;
                levelAudioSource.Play();
                levelAudioSource.time = playTime;
            }
        }
        else
        {
            if (levelAudioSource.clip != levelMusic)
            {

                float playTime = levelAudioSource.time;
                levelAudioSource.Stop();
                levelAudioSource.clip = levelMusic;
                levelAudioSource.Play();
                levelAudioSource.time = playTime;
            }
        }       
    }
}
