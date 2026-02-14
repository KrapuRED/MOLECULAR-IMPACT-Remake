using UnityEngine;
using System.Collections.Generic;

public class StatusManager : MonoBehaviour
{
    public static StatusManager instance;

    [Header("Status")]
    [Range(0, 100)]
    [SerializeField] private float _statusHappiness;
    [Range(0, 100)]
    [SerializeField] private float _statusSocial;
    [Range(0, 100)]
    [SerializeField] private float _statusFitness;
    [Range(0, 100)]
    [SerializeField] private float _statusIntelligence;
    [SerializeField] private int _statusCurrency;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    //TO CALCULATE EFFECTS FOR STATUS AND CURRENCY
    public void CalculatEffects(ActivitySO activity)
    {
        for (int i = 0; i < activity.benefits.Length; i++)
        {
            switch (activity.benefits[i].status)
            {
                case Status.Happiness:
                    //Debug.Log($"{activity.activityName} is giving Happiness {activity.benefits[i].valueBenefit}");
                    _statusHappiness += activity.benefits[i].valueBenefit;
                    continue;
                case Status.Fitness:
                    //Debug.Log($"{activity.activityName} is giving Fitness {activity.benefits[i].valueBenefit}");
                    _statusFitness += activity.benefits[i].valueBenefit;
                    continue;
                case Status.Intelligence:
                    //Debug.Log($"{activity.activityName} is giving Intelligence {activity.benefits[i].valueBenefit}");
                    _statusIntelligence += activity.benefits[i].valueBenefit;
                    continue;
                case Status.Social:
                    //Debug.Log($"{activity.activityName} is giving Social {activity.benefits[i].valueBenefit}");
                    _statusSocial += activity.benefits[i].valueBenefit;
                    continue;
                case Status.Currency:
                    //Debug.Log($"{activity.activityName} is giving Currency {activity.benefits[i].valueBenefit}");
                    _statusCurrency += activity.benefits[i].valueBenefit;
                    break;
                default:
                    break;
            }
        }
    }
}
