using UnityEngine;

public enum ActivityType
{
    None,
    Good,
    Bad
}

[System.Serializable]
public class Benefit
{
    public StatusSO statusData;
    public float valueBenefit;
}

[CreateAssetMenu(fileName = "_activityData", menuName = "Game Data/activitySO")]
public class ActivitySO : ScriptableObject
{
    public string activityID;
    public string activityName;
    public Sprite activityIcon;
    public ActivityType activityType;

    public Benefit[] benefits;

    [Header("Perks")]
    public PerkStatus[] perkStatus;
}
