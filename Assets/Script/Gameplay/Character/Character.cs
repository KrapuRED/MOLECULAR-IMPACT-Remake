using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Characeter Data")]
    [SerializeField] private CharacterDataSO characterData;

    public virtual void OnInteract()
    {
        Debug.Log("Interacted with " + characterData.characterName);
    }
}
