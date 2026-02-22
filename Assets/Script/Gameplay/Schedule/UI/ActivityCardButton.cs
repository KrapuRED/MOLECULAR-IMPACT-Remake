using UnityEngine;

public class ActivityCardButton : MonoBehaviour
{
    [SerializeField] private ActivityCardUI _activityCardUI;

    public void OnClickButton()
    {
        if (!_activityCardUI.isSelected)
        {
            ScheduleManager.instance.AddSelectedActivity(_activityCardUI.activityData);
            _activityCardUI.SelectedActivity();
        }
    }
}
