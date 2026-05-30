using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Characeter Data")]
    [SerializeField] protected CharacterDataSO characterData;

    public CharacterDataSO CharacterData => characterData;

    public virtual void OnInteract()
    {
        Debug.Log("Interacted with " + characterData.characterName);
    }
}
