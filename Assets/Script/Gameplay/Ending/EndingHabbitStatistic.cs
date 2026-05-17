using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndingHabbitStatistic : MonoBehaviour
{
    [SerializeField] private GameObject habbitPrefab;
    [SerializeField] private Transform spawner;

    private void Start()
    {
        SetUpTopHabbit();
    }

    public void SetUpTopHabbit()
    {
        List<EndingActivityData> sortedActivities =
            EndingHabbitDatabase.instance.GetEndingActivities()
            .OrderByDescending(x => x.activityCount)
            .Take(3)
            .ToList();

        for (int i = 0; i < sortedActivities.Count; i++)
        {
            GameObject obj = Instantiate(
                habbitPrefab,
                spawner
            );

            obj.GetComponent<TopHabbit>().SetUp(i , sortedActivities[i]);
        }
    }
}