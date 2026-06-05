using TMPro;
using UnityEngine;

public class PanelInteraction : Panel
{
    [SerializeField] private string characterID;

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private void OnEnable()
    {
        GlobalEvent.OnShowPanelInteraction.Addistener(Show);
        GlobalEvent.OnHidePanelInteraction.Addistener(Hide);
    }

    private void OnDisable()
    {
        RemoveListener();
    }
    private void OnDestroy()
    {
        RemoveListener();
    }

    private void RemoveListener()
    {
        GlobalEvent.OnShowPanelInteraction.Removeistener(Show);
        GlobalEvent.OnHidePanelInteraction.Removeistener(Hide);
    }

    public override void Show(string charID, object args2 = null)
    {
        if (this == null) return; // Check if the panel has been destroyed before trying to show it

        if (charID != characterID)
        {
            return; // If the character ID does not match, do not show the panel
        }

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        if (args2 != null)
        {
            titleText.text       = ((InteractionData)args2).titleInteraction;
            descriptionText.text = ((InteractionData)args2).descriptionInteraction;
        }
        
        InteractionManager.instance.SetInteractionPanel();
    }

    public override void Hide()
    {
        if (this == null) return; // Check if the panel has been destroyed before trying to show it

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        //Clear or Hide text and botton
        GlobalEvent.OnHideCommitmentBotton.Invoke();
        InteractionManager.instance.SetInteractionPanel();
        
    }
}
