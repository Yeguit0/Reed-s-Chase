using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject CreditsPanel;
    [SerializeField] GameObject SettingsPanel;
    [SerializeField] GameObject WarningPanel;
    private Animator anim;

    private bool settingsVisible = false;
    private bool creditsVisible = false;

    private void Start()
    {
        WarningPanel.SetActive(true);
        Time.timeScale = 0f;
        anim = GetComponent<Animator>();
    }

    public void CloseWarningPanel()
    {
        WarningPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void StartChange()
    {
        anim.SetTrigger("Start");
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Menu_OpenCredits()
    {
        if (settingsVisible)
        {
            Menu_OpenSettings();
        }
        creditsVisible = !creditsVisible;
        CreditsPanel.SetActive(creditsVisible);
        //StartCoroutine(CreditFunction());

    }

    public void Menu_OpenSettings()
    {
        settingsVisible = !settingsVisible;
        SettingsPanel.SetActive(settingsVisible);
    }

    public void Out_QuitGameButton()
    {
        Application.Quit();
    }
        
    //IEnumerator CreditFunction()
    //{
    //    CreditsPanel.SetActive(true);
    //    yield return new WaitForSeconds(10f);
    //    CreditsPanel.SetActive(false);
    //}
}
