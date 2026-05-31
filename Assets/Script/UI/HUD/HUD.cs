using UnityEngine;

public class HUD : MonoBehaviour
{
    public string panelID;

    public virtual void ShowHUD(bool isOpen)
    {

    }

    public virtual void HideHUD()
    {
        gameObject.SetActive(false);
    }
}
