using Unity.VisualScripting;
using UnityEngine;

public class CharacterBar : Character
{
    public override void OnInteract()
    {
        base.OnInteract();

        if (characterData.isProgessAble)
        {
            InteractionManager.instance.InteactionWithAnotherCharacter(characterData.characterID);

            GlobalEvent.OnShowCommitmentBotton.Invoke();
        }
        
        else
            {
            // Show panel with character name and description
            GlobalEvent.OnShowPanelInteraction.Invoke(characterData.characterID, null);
        }
    }
}
