using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour, ISetting
{
    [Header("Music Slider")]
    [SerializeField] AudioMixer musicMixer;
    [SerializeField] Slider musicSlider;
    [Header("SFX Slider")]
    [SerializeField] AudioMixer sfxMixer;
    [SerializeField] Slider sfxSlider;

    void Start()
    {
        SettingController.instance.settings.Add(this);
    }

    //------------------change values-----------------
    public void SetMusicVolume(float vol)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(vol) * 20);
    }

    public void SetSFXVolume(float vol)
    {
        sfxMixer.SetFloat("SFXVolume", Mathf.Log10(vol) * 20);
    }

    //-------------------save values-------------------
    public void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }

    //-------------------load values-------------------
    public void Load()
    {
        //music slider
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
        //sfx slider
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
    }
}