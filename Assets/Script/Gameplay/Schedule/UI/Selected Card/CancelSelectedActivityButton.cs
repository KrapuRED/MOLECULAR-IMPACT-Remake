using UnityEngine;

public class CancelSelectedActivityButton : MonoBehaviour
{
    public IconSelectedActivityUI iconSelectedActivityUI;
    public void OnClick()
    {
        ScheduleManager.instance.RemoveSelectedActivity(iconSelectedActivityUI.activityData);
        iconSelectedActivityUI.CancelActivity();
    }
}
