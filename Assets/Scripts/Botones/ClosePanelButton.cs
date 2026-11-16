using UnityEngine;

public class ClosePanelButton : MonoBehaviour
{
    public GameObject currentWindow;
    public GameObject newWindow;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePanel()
    {
        currentWindow.SetActive(false);
        newWindow.SetActive(true);
    }
}
