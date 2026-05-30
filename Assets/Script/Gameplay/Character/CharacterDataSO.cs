using UnityEngine;

public enum CharacterType
{
    None,
    Bartander,
    GirlBar,
    GirlLibrary,
    GirlGym
}

[CreateAssetMenu(fileName = "CharacterDataSO", menuName = "Character Data/CharacterDataSO")]
public class CharacterDataSO : ScriptableObject
{
    public string characterName;
    public CharacterType characterType;
}
