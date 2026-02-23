using UnityEngine;

public class ActivityCardButton : MonoBehaviour
{
    [SerializeField] private ActivityCardUI _activityCardUI;

    public void OnClickButton()
    {
        ScheduleManager.instance.AddSelectedActivity(_activityCardUI.activityData.activityID);
    }
}
