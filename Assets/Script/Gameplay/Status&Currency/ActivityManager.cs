using UnityEngine;
using System.Collections.Generic;

public class ActivityManager : MonoBehaviour
{
    public static ActivityManager instance;

    [SerializeField] private List<ActivitySO> _activities = new List<ActivitySO>();
    [SerializeField] private int _indexActivity;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
    }

    public void ActiveActivity()
    {
        if (_activities.Count <= 0)
        {
            Debug.LogWarning("There are no activities on ActivityManager");
            return;
        }
        if (_indexActivity >= _activities.Count)
        {
            Debug.Log($"Done with all activities!");
            return;
        }
        if (_activities[_indexActivity] == null)
        {
            Debug.Log($"There are no activity on {_indexActivity}");
            return;
        }


        StatusManager.instance.CalculatBenefitToStatus(_activities[_indexActivity]);
        PerkManager.instance.UpdatePerk(_activities[_indexActivity]);
        _indexActivity++;
    }

    public void AddActivity(List<ActivitySO> activityDatas)
    {
        if (activityDatas == null || activityDatas.Count <= 0)
        {
            Debug.LogWarning("Trying to add null activity on ActivityManager");
            return;
        }

        _activities.Clear();  

        foreach (var activityData in activityDatas)
            _activities.Add(activityData);
    }
}
