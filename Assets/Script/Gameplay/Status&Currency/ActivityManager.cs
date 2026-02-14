using UnityEngine;
using System.Collections.Generic;

public class ActivityManager : MonoBehaviour
{
    public static ActivityManager instance;

    public List<activitySO> activities = new List<activitySO>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ActiveActivity();
    }

    public void ActiveActivity()
    {
        StatusManager.instance.CalculatEffects(activities);
    }
}
