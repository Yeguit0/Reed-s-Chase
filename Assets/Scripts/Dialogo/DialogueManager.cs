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

    [Header("Chat (ScrollView)")]
    public ScrollRect scrollRect;
    public Transform content;               // El objeto Content del ScrollView
    public GameObject npcMessagePrefab;     // Burbuja del NPC
    public GameObject playerMessagePrefab;  // Burbuja del jugador

    [Header("Botones de respuesta")]
    public Button button1;
    public Button button2;
    public TextMeshProUGUI buttonText1;
    public TextMeshProUGUI buttonText2;

    private bool dialogueStarted = false;
    private bool isTyping = false;

    private DialogueNode currentNode;
    private Coroutine autoAdvanceCoroutine;

    void Start()
    {
        StartDialogue();
    }

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

        // Crea una burbuja nueva del NPC y escribe dentro de ella
        TMP_Text bubbleText = CreateMessage(npcMessagePrefab, "");

        string currentLine = currentNode.dialogueText;

        foreach (char letter in currentLine)
        {
            bubbleText.text += letter;
            ScrollToBottom();
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

        button1.onClick.AddListener(() => SelectChoice(currentNode.choice1, currentNode.nextDialogue1));
        button2.onClick.AddListener(() => SelectChoice(currentNode.choice2, currentNode.nextDialogue2));
    }

    private void SelectChoice(string chosenText, DialogueNode nextNode)
    {
        // Oculta los botones y muestra la respuesta como burbuja del jugador
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);

        CreateMessage(playerMessagePrefab, chosenText);
        ScrollToBottom();

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

    // ---------- Utilidades del chat ----------

    private TMP_Text CreateMessage(GameObject prefab, string text)
    {
        GameObject msg = Instantiate(prefab, content);
        TMP_Text tmp = msg.GetComponentInChildren<TMP_Text>();
        tmp.text = text;
        return tmp;
    }

    private void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}