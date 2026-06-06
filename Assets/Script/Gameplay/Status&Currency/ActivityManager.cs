using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public enum ActivityCategory
{
    None,
    Reading,
    Exercise,
    Drinking
}

[System.Serializable]
public class ActivityMultipliers
{
    public ActivityCategory activityCategory;
    public float multiplierValue = 1;
}

public class ActivityManager : MonoBehaviour
{
    public static ActivityManager instance;

    [SerializeField] private List<ActivityMultipliers> _activityMultipliers = new List<ActivityMultipliers>();

    [SerializeField] private int _indexActivity;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void OnDoingActivity()
    {
        List<ActivitySO> _activities = GameStateManager.instance.GetSelectedActivities();

        if (_activities.Count <= 0) { Debug.LogWarning("There are no activities on ActivityManager"); return; }
        if (_indexActivity >= _activities.Count) { Debug.Log("Done with all activities!"); _indexActivity = 0; return; }
        if (_activities[_indexActivity] == null) { Debug.Log($"No activity on {_indexActivity}"); return; }


        float multiplierValue = _activityMultipliers
       .Find(am => am.activityCategory == _activities[_indexActivity].activityCategory)
       ?.multiplierValue ?? 1f;

        GlobalEvent.OnShowIllustrastionWorkDay.Invoke(
        _activities[_indexActivity].activityIcon,
        _activities[_indexActivity].dialogueWorkDay);


        StatusManager.instance.CalculatBenefitToStatus(_activities[_indexActivity], multiplierValue);
        PerkManager.instance.UpdatePerkByActivity(_activities[_indexActivity]);

        _indexActivity++;
    }

    public void SetMultiplierByActivityCategory(ActivityCategory activityCategory, float multiplierValue)
    {
        var activityMultiplier = _activityMultipliers.Find(am => am.activityCategory == activityCategory);

        if (activityMultiplier == null)
        {
            return;
        }

        activityMultiplier.multiplierValue += multiplierValue;
    }
}
