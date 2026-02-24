using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public string panelID;

    public void OnClickButton()
    {
        HUDManager.instance.OpenPanel(panelID);
    }
}
