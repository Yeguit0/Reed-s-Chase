using System.Collections;
using TMPro;
using UnityEngine;

public class DialogoTrigger : MonoBehaviour
{
    [Header("Datos del Dialogo")]
    [SerializeField] private StructureData dialogueData;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float waitTimeBetweenLines = 1.5f; // Tiempo de espera antes de la siguiente línea

    [Header("Referencias de la UI")]
    //public GameObject indicador;
    public GameObject dialoguePanel;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI dialogueText;

    private bool dialogueStarted = false;
    private int currentLineIndex = 0;
    private bool isTyping = false;
    private Coroutine autoAdvanceCoroutine; // Para controlar el auto-avance

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !dialogueStarted)
        {
            StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EndDialogue();
        }
    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        //indicador.SetActive(false);
        dialogueStarted = true;
        currentLineIndex = 0;
        textName.text = dialogueData.npcName;

        StartCoroutine(DisplayCurrentLine());
    }

    private void NextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueData.dialogueLines.Count)
        {
            StartCoroutine(DisplayCurrentLine());
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        StopAllCoroutines(); // Detiene la escritura y el auto-avance inmediatamente
        dialoguePanel.SetActive(false);
        dialogueStarted = false;
        isTyping = false;
    }

    IEnumerator DisplayCurrentLine()
    {
        isTyping = true;
        dialogueText.text = "";
        string currentLine = dialogueData.dialogueLines[currentLineIndex];

        foreach (char letter in currentLine)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        // Iniciamos la corrutina de espera para pasar a la siguiente línea
        autoAdvanceCoroutine = StartCoroutine(WaitAndNextLine());
    }

    IEnumerator WaitAndNextLine()
    {
        yield return new WaitForSeconds(waitTimeBetweenLines);
        NextLine();
    }
}