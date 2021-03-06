using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private TMP_Text musicVolumeTextVal = null;
    [SerializeField] private Slider musicVolumeSlider = null;
    [SerializeField] private TMP_Text voiceVolumeTextVal = null;
    [SerializeField] private Slider voiceVolumeSlider = null;
    [SerializeField] private TMP_Text SFXVolumeTextVal = null;
    [SerializeField] private Slider SFXVolumeSlider = null;
    [SerializeField] private AudioSource[] musicAudio = null;
    [SerializeField] private AudioSource[] voiceAudio = null;
    [SerializeField] private AudioSource[] SFXAudio = null;

    private int firstPlayInt;
    private float musicFloat, voiceFloat, SFXFloat;

    // Start is called before the first frame update
    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt("FirstPlay");

        if(firstPlayInt == 0)
        {
            musicFloat = .5f;
            voiceFloat = .5f;
            musicVolumeSlider.value = musicFloat;
            voiceVolumeSlider.value = voiceFloat;
            SFXVolumeSlider.value = SFXFloat;
            musicVolumeTextVal.text = musicFloat.ToString("0.0");
            voiceVolumeTextVal.text = voiceFloat.ToString("0.0");
            SFXVolumeTextVal.text = SFXFloat.ToString("0.0");
            PlayerPrefs.SetFloat("MusicVolSetting", musicVolumeSlider.value);
            PlayerPrefs.SetFloat("VoiceVolSetting", voiceVolumeSlider.value);
            PlayerPrefs.SetFloat("SFXVolSetting", SFXVolumeSlider.value);
            PlayerPrefs.SetInt("FirstPlay", -1);
        }
        else
        {
            musicFloat = PlayerPrefs.GetFloat("MusicVolSetting");
            voiceFloat = PlayerPrefs.GetFloat("VoiceVolSetting");
            SFXFloat = PlayerPrefs.GetFloat("SFXVolSetting");
            musicVolumeSlider.value = musicFloat;
            voiceVolumeSlider.value = voiceFloat;
            SFXVolumeSlider.value = SFXFloat;
            musicVolumeTextVal.text = musicFloat.ToString("0.0");
            voiceVolumeTextVal.text = voiceFloat.ToString("0.0");
            SFXVolumeTextVal.text = SFXFloat.ToString("0.0");
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat("MusicVolSetting", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("VoiceVolSetting", voiceVolumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolSetting", SFXVolumeSlider.value);
    }

    void OnApplicationFocus(bool inFocus)
    {
        if(!inFocus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        for(int i = 0; i < musicAudio.Length; i++)
        {
            musicAudio[i].volume = musicVolumeSlider.value;
        }

        for(int i = 0; i < voiceAudio.Length; i++)
        {
            voiceAudio[i].volume = voiceVolumeSlider.value;
        }

        for(int i = 0; i < SFXAudio.Length; i++)
        {
            SFXAudio[i].volume = SFXVolumeSlider.value;
        }

        musicVolumeTextVal.text = musicVolumeSlider.value.ToString("0.0");
        voiceVolumeTextVal.text = voiceVolumeSlider.value.ToString("0.0");
        SFXVolumeTextVal.text = SFXVolumeSlider.value.ToString("0.0");
    }

    // public void SetVolume(float volume)
    // {
    //     AudioListener.volume = volume;
    //     volumeTextVal.text = volume.ToString("0.0");
    // }
}
