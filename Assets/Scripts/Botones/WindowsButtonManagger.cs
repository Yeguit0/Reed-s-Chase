using UnityEngine;

public class WindowsButtonManagger : MonoBehaviour
{
    public GameObject windowsPanel;
    private bool isPanelOpen = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        windowsPanel.SetActive(isPanelOpen);
    }

    public void OpenWindowsPanel()
    {
        isPanelOpen = !isPanelOpen;
    }
}
