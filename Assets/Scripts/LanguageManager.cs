using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    [Header("Idioma")]
    public bool isSpanish = false;

    [Header("Español")]
    public GameObject[] spanishObjects;

    [Header("English")]
    public GameObject[] englishObjects;

    void Start()
    {
        isSpanish = PlayerPrefs.GetInt("IsSpanish", 0) == 1;
        UpdateLanguage();
    }

    public void UpdateLanguage()
    {
        foreach (GameObject obj in spanishObjects)
        {
            obj.SetActive(isSpanish);
        }

        foreach (GameObject obj in englishObjects)
        {
            obj.SetActive(!isSpanish);
        }
    }

    public void ChangeLanguage()
    {
        isSpanish = !isSpanish;

        PlayerPrefs.SetInt("IsSpanish", isSpanish ? 1 : 0);
        PlayerPrefs.Save();

        UpdateLanguage();
    }
}