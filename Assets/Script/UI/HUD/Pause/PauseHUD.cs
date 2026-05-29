using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PauseHUD : HUD
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
