using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [Header("Volume Sliders")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    [Header("Music Toggle")]
    public Toggle musicMuteToggle;

    [Header("Scene")]
    public string mainMenuScene = "main-menu";

    private void Start()
    {
        LoadSettings();
    }

    public void OnMasterVolumeChanged()
    {
        AudioListener.volume = masterVolumeSlider.value;
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
    }

    public void OnMusicVolumeChanged()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
    }

    public void OnSFXVolumeChanged()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
    }

    public void OnMusicMuteToggled()
    {
        AudioListener.pause = musicMuteToggle.isOn;
        PlayerPrefs.SetInt("MusicMuted", musicMuteToggle.isOn ? 1 : 0);
    }

    public void OnBackClicked()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(mainMenuScene);
    }

    private void LoadSettings()
    {
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
            AudioListener.volume = masterVolumeSlider.value;
        }

        if (musicVolumeSlider != null)
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);

        if (sfxVolumeSlider != null)
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        if (musicMuteToggle != null)
            musicMuteToggle.isOn = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
    }
}