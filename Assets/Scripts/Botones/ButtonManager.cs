using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Buttons botonEng;
    public Buttons botonEs;
    public LanguageManager languageManager;

    [Header("Interfaz")]
    public TMP_Text textoNombre;
    public Image imagenItem;


    void Start()
    {
        textoNombre.text = botonEng.nombreItem;
        imagenItem.sprite = botonEs.portada;
    }

    void Update()
    {
        if (languageManager.isSpanish)
        {
            textoNombre.text = botonEs.nombreItem;
            imagenItem.sprite = botonEs.portada;
        }
        else
        {
            textoNombre.text = botonEng.nombreItem;
            imagenItem.sprite = botonEng.portada;
        }
    }
}
