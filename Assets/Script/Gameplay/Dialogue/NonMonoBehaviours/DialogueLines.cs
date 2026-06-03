using UnityEngine;

[System.Serializable]
public class DialogueLines
{
    public DialogueCharacter character;
    public Sprite backGroundImage;
    public bool onTheRight;
    [TextArea(3, 10)]
    public string line;
}
