using UnityEngine;

public enum ActivityType
{
    None,
    Good,
    Bad
}

public enum Status
{
    Happiness,
    Social,
    Fitness,
    Intelligence,
    Currency
}

[CreateAssetMenu(fileName = "_activityData", menuName = "Game Data/activitySO")]
public class ActivitySO : ScriptableObject
{
    public string activityID;
    public string activityName;
    public Sprite activityIcon;
    public ActivityType activityType;


    [System.Serializable]
    public class Benefit
    {
        public Status status;
        public int valueBenefit;
    }
    public Benefit[] benefits;

    [Header("Perks")]
    public PerkStatus[] perkStatus;
}
