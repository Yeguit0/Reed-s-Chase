using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueNode", menuName = "Scriptable Objects/OpeningDialogue")]
public class OpeningDialogue : ScriptableObject
{
    [Header("Texto")]
    [TextArea(3, 5)]
    public string nameText;
    public string dialogueText;

    [Header("Respuestas")]
    public bool hasChoices;

    public string choice1;
    public OpeningDialogue nextDialogue1;

    public string choice2;
    public OpeningDialogue nextDialogue2;

    public string choice3;
    public OpeningDialogue nextDialogue3;

    [Header("Siguiente diálogo")]
    public OpeningDialogue nextDialogue;
}
