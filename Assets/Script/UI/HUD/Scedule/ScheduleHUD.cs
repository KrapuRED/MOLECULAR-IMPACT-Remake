using UnityEngine;

public class ScheduleHUD : HUD
{
    public override void ShowPanel(bool isOpen)
    {
        gameObject.SetActive(isOpen);
    }

    public override void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
