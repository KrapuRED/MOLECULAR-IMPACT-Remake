using UnityEngine;
using TMPro;
public class PanelNoMeetRequirment : Panel
{
    [SerializeField] private TextMeshProUGUI titlePerkNameText;
    [SerializeField] private TextMeshProUGUI descriptionNoMeetReqText;

    private void OnEnable()
    {
        GlobalEvent.OnShowPanelNoMeetRequirment.Addistener(Show);
        GlobalEvent.OnHidePanelNoMeetRequirment.Addistener(Hide);
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
        GlobalEvent.OnShowPanelNoMeetRequirment.Removeistener(Show);
        GlobalEvent.OnHidePanelNoMeetRequirment.Removeistener(Hide);
    }

    public override void Show(string charID, object args2 = null)
    {
        if  (this == null) return; // Check if the panel has been destroyed before trying to show it

        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        if (args2 == null)
        {
            Debug.LogWarning("[PanelNoMeetRequirment] Show called without args2. Expected commitment name as string.");
            return;
        }

        CommitmentData commitment = ((CommitmentData)args2);

        descriptionNoMeetReqText.text = string.Empty;

        foreach (var perk in commitment.requirmentPerks)
        {
            descriptionNoMeetReqText.text += $"- Aquire {perk.perkName} Tag\n";
        }

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public override void Hide()
    {
        if (this == null) return; // Check if the panel has been destroyed before trying to show it

        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
