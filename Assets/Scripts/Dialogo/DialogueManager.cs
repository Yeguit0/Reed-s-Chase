using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("Datos del Dialogo")]
    [SerializeField] private DialogueNode dialogueData;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float waitTimeBetweenLines = 1.5f;

    [Header("Referencias de la UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    public Button button1;
    public Button button2;

    public TextMeshProUGUI buttonText1;
    public TextMeshProUGUI buttonText2;

    private bool dialogueStarted = false;
    private bool isTyping = false;

    private DialogueNode currentNode;
    private Coroutine autoAdvanceCoroutine;

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        dialogueStarted = true;

        currentNode = dialogueData;

        DisplayCurrentNode();
    }

    private void DisplayCurrentNode()
    {
        StopAllCoroutines();

        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);

        StartCoroutine(DisplayText());
    }

    IEnumerator DisplayText()
    {
        isTyping = true;
        dialogueText.text = "";

        string currentLine = currentNode.dialogueText;

        foreach (char letter in currentLine)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        if (currentNode.hasChoices)
        {
            ShowChoices();
        }
        else
        {
            autoAdvanceCoroutine = StartCoroutine(WaitAndNextLine());
        }
    }

    private void ShowChoices()
    {
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);

        buttonText1.text = currentNode.choice1;
        buttonText2.text = currentNode.choice2;

        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();

        button1.onClick.AddListener(() => SelectChoice(currentNode.nextDialogue1));
        button2.onClick.AddListener(() => SelectChoice(currentNode.nextDialogue2));
    }

    private void SelectChoice(DialogueNode nextNode)
    {
        if (nextNode == null)
        {
            EndDialogue();
            return;
        }

        currentNode = nextNode;
        DisplayCurrentNode();
    }

    IEnumerator WaitAndNextLine()
    {
        yield return new WaitForSeconds(waitTimeBetweenLines);

        EndDialogue();
    }

    private void EndDialogue()
    {
        StopAllCoroutines();

        dialogueStarted = false;
        isTyping = false;
    }

    public void CloseDialogue()
    {
        EndDialogue();
    }
}
