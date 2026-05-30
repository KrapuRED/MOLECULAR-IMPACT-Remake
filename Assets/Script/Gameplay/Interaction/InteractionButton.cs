using UnityEngine;

public class InteractionButton : MonoBehaviour
{
    [SerializeField] private Character interactAbleChar;

    public void OnInteractionButtonClicked()
    {
        if (interactAbleChar != null)
        {
            InteractionManager.instance.ConfiremedInteraction(interactAbleChar.CharacterData.characterName , interactAbleChar.CharacterData.characterID);
        }
        else
        {
            Debug.LogError("Interactable character is not assigned in the inspector.");
        }
    }
}
