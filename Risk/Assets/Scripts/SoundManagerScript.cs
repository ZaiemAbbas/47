﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript SoundInstance;
    public List<AudioClip> Sounds;
    public List<AudioClip> MainSounds;
    static AudioSource audiosrc;
    // Start is called before the first frame update
    void Awake()
    {
        audiosrc = GetComponent<AudioSource>();
        SoundInstance = this;
      //  BackMusic();
    }

    public void PlaySword()
    {
        audiosrc.PlayOneShot(Sounds[4],0.7f);
    }
    public void PlayBoom()
    {
        audiosrc.PlayOneShot(Sounds[5], 0.7f);
    }
    public void DiceRoll()
    {
        audiosrc.PlayOneShot(Sounds[6], 0.7f);
    }
    public void BackMusic()
    {
        audiosrc.clip = Sounds[3];
        audiosrc.volume = 0.7f;
        audiosrc.loop = true;
        audiosrc.Play();
    }
    public void MainMenuMusic(bool t)
    {
        if (t)
        {
            audiosrc.clip = Sounds[3];
            audiosrc.loop = true;
            audiosrc.Play();
            //audiosrc.PlayOneShot(Sounds[3]);
           
        }
        else
            audiosrc.Stop();
    }
}