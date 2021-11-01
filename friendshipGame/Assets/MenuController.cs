using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    // [Header("Volume Settings")]
    // [SerializeField] private TMP_Text musicVolumeTextVal = null;
    // [SerializeField] private Slider musicVolumeSlider = null;
    // [SerializeField] private TMP_Text voiceVolumeTextVal = null;
    // [SerializeField] private Slider voiceVolumeSlider = null;

    // [SerializeField] private GameObject confirmationPrompt = null;

    [Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    public void newGameDialogYes() 
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void loadGameDialogYes() 
    {
        if (PlayerPrefs.HasKey("SavedLevel1"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel1");
            SceneManager.LoadScene(levelToLoad);
        } 
        else 
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    // public void SetVolume(float volume)
    // {
    //     AudioListener.volume = volume;
    //     volumeTextVal.text = volume.ToString("0.0");
    // }

    // public void VolumeApply()
    // {
    //     PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    //     // PlayerPrefs.SetBool("voiceActing", true);
    //     StartCoroutine(ConfirmationBox());
    // }

    // public IEnumerator ConfirmationBox()
    // {
    //     confirmationPrompt.SetActive(true);
    //     yield return new WaitForSeconds(2);
    //     confirmationPrompt.SetActive(false);
    // }
}
