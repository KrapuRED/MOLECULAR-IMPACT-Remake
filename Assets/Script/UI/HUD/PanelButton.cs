using UnityEngine;

public class PanelButton : MonoBehaviour
{
    public string panelID;

    public void OnClickButton()
    {
        HUDManager.instance.OpenPanel(panelID);
    }

    public void OnClickCloseButton()
    {
        HUDManager.instance.ClosePanelByID(panelID);
    }
}
