using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Buttons boton;

    [Header("Interfaz")]
    public TMP_Text textoNombre;
    public Image imagenItem;


    void Start()
    {
        textoNombre.text = boton.nombreItem;
        imagenItem.sprite = boton.portada;
    }
}
