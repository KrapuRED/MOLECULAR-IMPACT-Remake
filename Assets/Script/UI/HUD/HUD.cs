using UnityEngine;

public class HUD : MonoBehaviour
{
    public string panelID;

    public virtual void ShowPanel(bool isOpen)
    {

    }

    public virtual void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
