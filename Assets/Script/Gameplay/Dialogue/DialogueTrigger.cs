using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class DialogueTrigger : ScriptableObject
{
    public Dialogue dialogue;
    public List<DialogueTrigger> dialogueChoices;

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue, dialogueChoices);
    }
}
