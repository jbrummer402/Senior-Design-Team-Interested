using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioSettings : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private TMP_Text musicVolumeTextVal = null;
    [SerializeField] private TMP_Text voiceVolumeTextVal = null;
    [SerializeField] private TMP_Text SFXVolumeTextVal = null;
    [SerializeField] private AudioSource[] musicAudio = null;
    [SerializeField] private AudioSource[] voiceAudio = null;
    [SerializeField] private AudioSource[] SFXAudio = null;
    private float musicFloat, voiceFloat, SFXFloat;

    void Awake()
    {
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        musicFloat = PlayerPrefs.GetFloat("MusicVolSetting");
        voiceFloat = PlayerPrefs.GetFloat("VoiceVolSetting");
        SFXFloat = PlayerPrefs.GetFloat("SFXVolSetting");

        for(int i = 0; i < musicAudio.Length; i++)
        {
            musicAudio[i].volume = musicFloat;
        }

        for(int i = 0; i < voiceAudio.Length; i++)
        {
            voiceAudio[i].volume = voiceFloat;
        }

        for(int i = 0; i < SFXAudio.Length; i++)
        {
            SFXAudio[i].volume = SFXFloat;
        }

        musicVolumeTextVal.text = musicFloat.ToString("0.0");
        voiceVolumeTextVal.text = voiceFloat.ToString("0.0");
        SFXVolumeTextVal.text = SFXFloat.ToString("0.0");
    }
}
