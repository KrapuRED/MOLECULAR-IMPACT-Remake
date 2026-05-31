using UnityEngine;

public class ScheduleHUD : HUD
{
    public CanvasGroup canvasGroup;

    public override void ShowHUD(bool isOpen)
    {
        if (canvasGroup == null) 
            canvasGroup = GetComponent<CanvasGroup>();
        
        canvasGroup.alpha = isOpen ? 1 : 0;
        canvasGroup.interactable = isOpen;
        canvasGroup.blocksRaycasts = isOpen;
    }

    public override void HideHUD()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
