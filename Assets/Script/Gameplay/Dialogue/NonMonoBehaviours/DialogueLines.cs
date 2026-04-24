using UnityEngine;

[System.Serializable]
public class DialogueLines
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}
