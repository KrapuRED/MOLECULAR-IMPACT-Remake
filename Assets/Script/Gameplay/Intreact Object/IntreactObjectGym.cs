using UnityEngine;

public class IntreactObjectGym : IntreactObject
{
    public override void OnIntreactObject()
    {
        if (DialogueManager.instance.IsDialogueActive || InteractionManager.instance.isPanelInteractionActive)
            return;

        GlobalEvent.OnShowPanelInteractionAnother.Invoke(objectID, null);
    }
}
