using UnityEngine;

public class PanelButton : MonoBehaviour
{
    public string panelID;

    public void OnClickButton()
    {
        HUDManager.instance.OpenPanel(panelID);
    }
}
