using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Scenes")]
    [SerializeField] string SceneChanger;

    [Header("UI")]
    [SerializeField] GameObject CreditsPanel;
    [SerializeField] GameObject SettingsPanel;

    private bool settingsVisible = false;

    public void ChangeScene(string SceneToLoad)
    {
        SceneManager.LoadScene(SceneToLoad);
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
