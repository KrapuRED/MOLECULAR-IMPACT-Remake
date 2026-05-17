using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EndingActivityData
{
    public ActivitySO activitySO;
    public int activityCount;
}

public class EndingHabbitDatabase : MonoBehaviour
{
    public static EndingHabbitDatabase instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private List<EndingActivityData> endingActivitiesData = new();



    //Nanti aku tambahin di SheduleManager -> ReadyActivity
    public void AddActivity(List<ActivitySO> activityDatas)
    {
        if (activityDatas == null || activityDatas.Count <= 0)
        {
            Debug.LogWarning("Trying to add null activity on ActivityManager");
            return;
        }

        foreach (ActivitySO activityData in activityDatas)
        {
            EndingActivityData existingData =
                endingActivitiesData.Find(x => x.activitySO == activityData);

            if (existingData != null)
            {
                existingData.activityCount++;
            }
            else
            {
                endingActivitiesData.Add(new EndingActivityData
                {
                    activitySO = activityData,
                    activityCount = 1
                });
            }
        }
    }

    public List<EndingActivityData> GetEndingActivities()
    {
        return endingActivitiesData;
    }


}