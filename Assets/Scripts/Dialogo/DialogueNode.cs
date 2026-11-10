using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueNode", menuName = "Dialogue/Dialogue Node")]
public class DialogueNode : ScriptableObject
{
    [Header("Texto")]
    [TextArea(3, 5)]
    public string dialogueText;

    [Header("Respuestas")]
    public bool hasChoices;

    public string choice1;
    public DialogueNode nextDialogue1;

    public string choice2;
    public DialogueNode nextDialogue2;
}
