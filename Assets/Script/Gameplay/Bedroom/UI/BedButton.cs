using UnityEngine;

public class BedButton : MonoBehaviour
{
    public void OnClick()
    {
        ScheduleManager.instance.SubmitActivityIntoEndingManager();
        GlobalEvent.OnNextDay.Invoke();
        GameManager.instance.NextGamePlay();
    }
}
