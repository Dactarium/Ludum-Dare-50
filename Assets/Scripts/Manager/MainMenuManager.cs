using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;

    [Header("Visual")]
    [SerializeField] private Toggle _filmGrainToggle;
    [SerializeField] private Slider _zoomSlider;
   
    void Start(){
        _masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0f);
        _musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", -10f);
        _sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0f);

        _mixer.SetFloat("Master", _masterVolumeSlider.value);
        _mixer.SetFloat("Music", _musicVolumeSlider.value);
        _mixer.SetFloat("SFX", _sfxVolumeSlider.value);

        _filmGrainToggle.isOn = PlayerPrefs.GetInt("FilmGrain", 1) == 1;
        _zoomSlider.value = PlayerPrefs.GetFloat("Zoom", 15f);
    }
    
    public void Play(){
        SceneManager.LoadScene(1);
    }

    public void Exit(){
        Application.Quit();
    }

    public void ChangeMasterVolume(float value){
        PlayerPrefs.SetFloat("MasterVolume", value);
        _mixer.SetFloat("Master", value);
    }

    public void ChangeMusicVolume(float value){
        PlayerPrefs.SetFloat("MusicVolume", value);
        _mixer.SetFloat("Music", value);
        
    }

    public void ChangeSFXVolume(float value){
        PlayerPrefs.SetFloat("SFXVolume", value);
        _mixer.SetFloat("SFX", value);
    }

    public void EnableFilmGrain(bool enable){
        PlayerPrefs.SetInt("FilmGrain", enable?1:0);
    }

    public void ChangeZoomValue(float value){
        PlayerPrefs.SetFloat("Zoom", value);
    }

}

   
