using UnityEngine;

public class SceduleHUD : HUD
{
    public override void ShowPanel(bool isOpen)
    {
        gameObject.SetActive(isOpen);
    }

    public override void HidePanel(bool isOpen)
    {
        gameObject.SetActive(isOpen);
    }
}
