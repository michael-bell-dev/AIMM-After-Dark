using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public AudioSource music, click;

    void Start()
    {
        music.volume = 0.6f * PlayerPrefs.GetFloat("musicVolume", 1f);
        click.volume = 0.5f * PlayerPrefs.GetFloat("sfxVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 1f);

        musicSlider.onValueChanged.AddListener(value =>
        {
            PlayerPrefs.SetFloat("musicVolume", value);
            music.volume = 0.6f * value;
            click.Play();
        });

        sfxSlider.onValueChanged.AddListener(value =>
        {
            PlayerPrefs.SetFloat("sfxVolume", value);
            click.volume = 0.5f * value;
            click.Play();
        });
    }
}