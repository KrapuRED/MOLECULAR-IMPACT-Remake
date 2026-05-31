using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PauseHUD : HUD
{
    public override void ShowHUD(bool isOpen)
    {
        gameObject.SetActive(isOpen);
    }

    public override void HideHUD()
    {
        gameObject.SetActive(false);
    }
}
