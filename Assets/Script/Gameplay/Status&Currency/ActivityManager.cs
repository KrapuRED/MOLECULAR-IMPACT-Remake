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
        ActiveActivity();
    }

    public void ActiveActivity()
    {
        StatusManager.instance.CalculatEffects(_activities[_indexActivity]);
        PerkManager.instance.UpdatePerk(_activities[_indexActivity]);
    }
}
