using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public bool isPaused = false;
    public bool isOnSettings = false;
    public bool canPause = true;

    void Start()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    void Update()
    {
        pausePanel.SetActive(isPaused);
        settingsPanel.SetActive(isOnSettings);

        if(isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (isOnSettings)
        {
            return;
        }
        isPaused = !isPaused;
    }

    public void OnSettings()
    {
        if(isPaused)
        {
            isOnSettings = !isOnSettings;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
