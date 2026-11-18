
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FirstSceneDialogue : MonoBehaviour
{
    [Header("Datos del Dialogo")]
    [SerializeField] private OpeningDialogue dialogueData;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float waitTimeBetweenLines = 1.5f;

    [Header("Referencias de la UI")]
    public GameObject dialoguePanel;
    public Animator characters;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI dialogueText;
    private Animator anim;
    public GameObject faceAnim;
    private Animator faceAnimator;

    public Button button1;
    public Button button2;
    public Button button3;

    public TextMeshProUGUI buttonText1;
    public TextMeshProUGUI buttonText2;
    public TextMeshProUGUI buttonText3;

    private bool dialogueStarted = false;
    private bool isTyping = false;

    private OpeningDialogue currentNode;
    private OpeningDialogue localizedNode;

    private Coroutine autoAdvanceCoroutine;

    private void Start()
    {
        anim = GetComponent<Animator>();
        faceAnimator = faceAnim.GetComponent<Animator>();
        characters.gameObject.SetActive(true);
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
        button3.gameObject.SetActive(false);

        characters.gameObject.SetActive(true);

        // Seleccionar idioma
        bool isSpanish = PlayerPrefs.GetInt("IsSpanish", 0) == 1;

        localizedNode = isSpanish && currentNode.otherDialogue != null
            ? currentNode.otherDialogue
            : currentNode;

        if (localizedNode.faceAnimation != null)
        {
            faceAnimator.runtimeAnimatorController = localizedNode.faceAnimation;
        }

        StartCoroutine(DisplayText());
    }

    IEnumerator DisplayText()
    {
        isTyping = true;
        dialogueText.text = "";
        characterNameText.text = localizedNode.nameText;

        string currentLine = localizedNode.dialogueText;

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
        button1.gameObject.SetActive(currentNode.nextDialogue1 != null);
        button2.gameObject.SetActive(currentNode.nextDialogue2 != null);
        button3.gameObject.SetActive(currentNode.nextDialogue3 != null);

        buttonText1.text = localizedNode.choice1;
        buttonText2.text = localizedNode.choice2;
        buttonText3.text = localizedNode.choice3;

        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();

        button1.onClick.AddListener(() => SelectChoice(currentNode.nextDialogue1));
        button2.onClick.AddListener(() => SelectChoice(currentNode.nextDialogue2));
        button3.onClick.AddListener(() => SelectChoice(currentNode.nextDialogue3));
    }

    private void SelectChoice(OpeningDialogue nextNode)
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

        if (currentNode.nextDialogue != null)
        {
            currentNode = currentNode.nextDialogue;
            DisplayCurrentNode();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        StopAllCoroutines();

        dialogueStarted = false;
        isTyping = false;
        dialoguePanel.SetActive(false);
        characters.gameObject.SetActive(false);
        anim.SetTrigger("End");
    }

    public void CloseDialogue()
    {
        EndDialogue();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(2);
    }
}
