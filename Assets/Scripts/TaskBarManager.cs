
using UnityEngine;
using UnityEngine.UI;

public class TaskBarManager : MonoBehaviour
{
    [Header("Taskbar")]
    public GameObject taskbarButton;

    private Button button;

    private void Start()
    {
        button = taskbarButton.GetComponent<Button>();

        taskbarButton.SetActive(false);

        button.onClick.AddListener(ToggleWindow);
    }

    public void OpenWindow()
    {
        gameObject.SetActive(true);
        taskbarButton.SetActive(true);
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
        taskbarButton.SetActive(false);
    }

    public void ToggleWindow()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
