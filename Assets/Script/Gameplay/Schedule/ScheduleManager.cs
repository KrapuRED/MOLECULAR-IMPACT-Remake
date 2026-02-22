using UnityEngine;
using System.Collections.Generic;

public class ScheduleManager : MonoBehaviour
{
    public static ScheduleManager instance;

    [Header("Activities Config")]
    [SerializeField] private Transform _containerActivities;
    [SerializeField] private ActivitySO[] _activityDatas;
    [SerializeField] private GenerateActivityCardUI _generateActivityCardUI;

    [Header("Selected Activities Config")]
    [SerializeField] private int _maxActivities;
    [SerializeField] private Transform _containerSelectedActivities;
    [SerializeField] private List<ActivitySO> _selectedActivityDatas = new List<ActivitySO>();
    [SerializeField] private GenerateSelectedActivityCardUI _generateSelectedActivityCardUI;

    [Header("Events")]
    [SerializeField] private TotalActivitiesCountEventSO _totalActivitiesCountEvent;
    [SerializeField] private CancelSelectedActivityEventSO _cancelSelectedActivityEvent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GenerateActvitiesByData();
    }

    //Fill ActivitiesUI with the activityDatas
    private void GenerateActvitiesByData()
    {
        foreach (var activityData in _activityDatas)
        {
            _generateActivityCardUI.GenerateActivityCard(activityData, _containerActivities);
        }
    }

    //Add Selected Activities to List
    public void AddSelectedActivity(ActivitySO activityData)
    {
        if (_selectedActivityDatas.Count >= _maxActivities)
            return;

        _selectedActivityDatas.Add(activityData);
        //Generate Icon for Selected Activity
        _generateSelectedActivityCardUI.GenerateSelectedActivityCard(activityData, _containerSelectedActivities);
        ScheduleManagerUpdateUI();
    }

    //Remove Selected Activities from List
    public void RemoveSelectedActivity(ActivitySO activityData)
    {
        for (int i = _selectedActivityDatas.Count - 1; i >= 0; i--)
        {
            if (_selectedActivityDatas[i].activityID == activityData.activityID)
            {
                _selectedActivityDatas.RemoveAt(i);
                //call event to that card is able to pick again
                _cancelSelectedActivityEvent.OnRaise(activityData);
                ScheduleManagerUpdateUI();
                break;
            }
        }
    }

    public void ScheduleManagerUpdateUI()
    {
        _totalActivitiesCountEvent.OnRaise(_selectedActivityDatas.Count, _maxActivities);
    }
}
