using UnityEngine;

public class PauseHUD : HUD
{
    public override void ShowPanel(bool isOpen)
    {
        gameObject.SetActive(isOpen);
    }
}
