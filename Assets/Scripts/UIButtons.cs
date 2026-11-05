using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public GameObject background;
    public GameObject window;

    public float doubleClickTime = 0.3f;

    private float lastClickTime;
    private bool isSelected;

    private void Start()
    {
        background.SetActive(false);
    }
    public void OnClick()
    {
        if (Time.time - lastClickTime <= doubleClickTime)
        {
            OpenPanel();
        }
        else
        {
            SelectButton();
        }

        lastClickTime = Time.time;
    }

    void SelectButton()
    {
        isSelected = true;

        if (background != null)
        {
            background.SetActive(true);
        }
    }

    void OpenPanel()
    {
        if (window != null)
        {
            window.SetActive(true);
            background.SetActive(false);
        }

    }

    public void DeselectButton()
    {
        isSelected = false;

        if (background != null)
            background.SetActive(false);
    }
}
