using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene Names")]
    public string newGameScene = "SampleScene";
    public string settingsScene = "Settings_panel";
    public string creditsScene = "Credits_panel";
    public string quitScene = "Quit_panel";

    [Header("Audio (Optional)")]
    public AudioSource buttonClickSound;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnNewGameClicked()
    {
        PlayClickSound();
        SceneManager.LoadScene(newGameScene);
    }

    public void OnSettingsClicked()
    {
        PlayClickSound();
        SceneManager.LoadScene(settingsScene);
    }

    public void OnCreditsClicked()
    {
        PlayClickSound();
        SceneManager.LoadScene(creditsScene);
    }

    public void OnQuitClicked()
    {
        PlayClickSound();
        SceneManager.LoadScene(quitScene);
    }

    private void PlayClickSound()
    {
        if (buttonClickSound != null)
        {
            buttonClickSound.Play();
        }
    }
}