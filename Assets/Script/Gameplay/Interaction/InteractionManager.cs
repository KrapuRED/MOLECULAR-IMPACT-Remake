using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class InteractionData
{
    public string titleInteraction;
    public string characterID;
    public int minInteractionCount;
    public string descriptionInteraction;
    public DialogueTrigger  dialogueTrigger;
}

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager instance;

    [SerializeField] private List<InteractionData> interactionDataList = new();
    [SerializeField] private PanelInteraction panelInteraction;

    public bool isPanelInteractionActive { get; private set; }

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

    private void Start()
    {
        StatusManager.instance.UpdateUI();
        CurrencyManager.instance.UpdateUI();
        DayCycleManager.instance.UpdateUI();
        PerkManager.instance.UpdeteUI();
    }

    public void InteactionWithAnotherCharacter(string characterID)
    {
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

    public void ConfiremedInteraction(string charName, string charID, int consume)
    {
        // Consume Energy
        CurrencyManager.instance.ConsumeEnergy(consume);
        
        GameStateManager.instance.SetInteractionDataGameState(charName, charID); //interaction count++
        int interactionCount =  GameStateManager.instance.GetInteractionCharacterByID(charID);
        
        var interactionData = interactionDataList.Find(interactionData => interactionData.characterID == charID);

        interactionData.dialogueTrigger.TriggerDialogue();
        //GameStateManager.instance.SetInteractionDataGameState(charName, charID);
    }
    
    private void ShowInteractionResultPanel(InteractionData data, string characterID)
    {
        panelInteraction.Show(characterID, data);
    }

    public void SetInteractionPanel()
    {
        Debug.Log($"SetInteractionPanel : {isPanelInteractionActive}" );
       if (isPanelInteractionActive)
           isPanelInteractionActive = false;
       else
           isPanelInteractionActive = true;
    }
}
