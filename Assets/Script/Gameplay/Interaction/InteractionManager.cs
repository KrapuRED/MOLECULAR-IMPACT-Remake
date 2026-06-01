using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class InteractionData
{
    public int minInteractionCount;
    public string titleInteraction;
    public string descriptionInteraction;
}

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager instance;

    [SerializeField] private List<InteractionData> interactionDataList = new();
    [SerializeField] private PanelInteraction panelInteraction;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InteactionWithAnotherCharacter(string characterID)
    {
        /*
         TO DO:
        1) Check if the character is already in the game state, if not add it to the game state with interaction count 1, if yes increment the interaction count by 1
        2) Show the interaction result panel with the character name
         */

        InteractionData intrectionData = null;

        if (GameStateManager.instance.IsInteractionCharacterExist(characterID))
        {
            Debug.Log("Character already exists in game state, incrementing interaction count");
        }
        else
        {
            Debug.Log("Character does not exist in game state, adding character with interaction count 1");
            intrectionData = interactionDataList[0];
        }

        ShowInteractionResultPanel(intrectionData, characterID);
    }

    private void ShowInteractionResultPanel(InteractionData data, string characterID)
    {
        panelInteraction.Show(characterID, data);
    }

    public void ConfiremedInteraction(string charName, string charID, int consume)
    {
        // Consume Energy
        CurrencyManager.instance.ConsumeEnergy(consume);

        GameStateManager.instance.SetInteractionDataGameState(charName, charID);
    }
}
