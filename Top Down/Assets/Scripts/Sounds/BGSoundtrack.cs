﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BGSoundtrack : MonoBehaviour
{
    public AudioSource Track0;
    public AudioSource Track1;
    public AudioSource Track2;

    public int TrackSelector;
    public int TrackHistory;

    // Start is called before the first frame update
    void Start()
    {
        TrackSelector = Random.Range(0,3);
        if (TrackSelector == 0)
        {
            Track0.Play();
            TrackHistory = 0;

        } else if (TrackSelector == 1)
        {
            Track1.Play();
            TrackHistory = 1;

        }else if(TrackSelector == 2)
        {
            Track2.Play();
            TrackHistory = 2;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (Track0.isPlaying == false && Track1.isPlaying == false && Track2.isPlaying == false)
        {
            TrackSelector = Random.Range(0,3);
            
            if(TrackSelector == 0 && TrackHistory != 0){
                Track0.Play();
                TrackHistory = 0;
            }else if (TrackSelector == 1 && TrackHistory != 1){
                Track1.Play();
                TrackHistory = 1;
            }else if (TrackSelector == 2 && TrackHistory != 2){
                Track2.Play();
                TrackHistory = 2;
            }

        }

        if (SceneManager.GetActiveScene().name == "0.MainMenu")
        {
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name == "CutsceneFinal")
        {
            Destroy(gameObject);
        }

    }
}
