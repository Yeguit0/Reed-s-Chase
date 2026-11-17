using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject CreditsPanel;
    [SerializeField] GameObject SettingsPanel;
    private Animator anim;

    private bool settingsVisible = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
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
        StartCoroutine(CreditFunction());
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
        
    IEnumerator CreditFunction()
    {
        CreditsPanel.SetActive(true);
        yield return new WaitForSeconds(10f);
        CreditsPanel.SetActive(false);
    }
}
