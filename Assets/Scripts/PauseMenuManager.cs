using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject gamePanel;
    public bool isPaused = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPaused)
        {
            pausePanel.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            pausePanel.SetActive(false);
            gameObject.SetActive(true);
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isPaused = !isPaused;
        }
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
    }
}
