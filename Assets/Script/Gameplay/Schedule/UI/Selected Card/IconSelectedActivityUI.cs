using UnityEngine;
using UnityEngine.UI;

public class IconSelectedActivityUI : MonoBehaviour
{
    [SerializeField] private ActivitySO _activityData;
    public ActivitySO activityData => _activityData;

    [SerializeField] private Image _activityIcon;

    public void SetSelectedActivityUI(ActivitySO activityData)
    {
        if (_activityData == null)
            _activityData = activityData;

        _activityIcon.sprite = activityData.activityIcon;
    }

    public void CancelActivity()
    {
        Destroy(gameObject);
    }
}
