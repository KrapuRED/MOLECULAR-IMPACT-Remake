using UnityEngine;

public class HidePanelNoMeetRequirmentButton : MonoBehaviour
{
    public void OnHidePanelNoMeetRequirmentButtonClick()
    {
        GlobalEvent.OnHidePanelNoMeetRequirment.Invoke();
    }
}
