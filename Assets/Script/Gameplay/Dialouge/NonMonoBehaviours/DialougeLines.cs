using UnityEngine;

[System.Serializable]
public class DialougeLines
{
    public DialougeCharacter character;
    [TextArea(3, 10)]
    public string line;
}
