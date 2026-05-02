using UnityEngine;

[System.Serializable]
public class DialogueLines
{
    public DialogueCharacter character;
    public bool onTheRight;
    [TextArea(3, 10)]
    public string line;
}
