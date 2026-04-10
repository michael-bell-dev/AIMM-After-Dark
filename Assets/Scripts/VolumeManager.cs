using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public AudioSource[] sfx;
    public AudioSource music;

    void Start()
    {
        if (music != null)
            music.volume = PlayerPrefs.GetFloat("musicVolume", 1f);

        float sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 1f);

        foreach (AudioSource sfxSource in sfx)
        {
            sfxSource.volume *= sfxVolume;
        }
    }
}