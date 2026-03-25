using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject pauseMenuPanel;
    public GameObject pauseButton;

    [Header("Scene Names")]
    public string mainMenuScene = "main-menu";

    private bool isPaused = false;

    private void Start()
    {
        // Make sure pause panel is hidden and pause button is visible at start
        pauseMenuPanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        // Press ESC to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnRestartClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenuClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

    public void OnExitClicked()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("Game Quit!");
    }
}