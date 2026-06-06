using UnityEngine;

public enum ActivityType
{
    None,
    Good,
    Bad
}



[CreateAssetMenu(fileName = "activityData", menuName = "Game Data/activitySO")]
public class ActivitySO : ScriptableObject
{
    public string activityID;
    public string activityName;
    public Sprite activityIcon;

    [Header("Activty Enum Config")]
    public ActivityType activityType;
    public ActivityCategory activityCategory;

    public string dialogueWorkDay;

    [System.Serializable]
    public class Benefit
    {
        public StatusSO statusData;
        public float valueBenefit;
    }

    [Header("Activty Benefit Config")]
    public Benefit[] benefits;

    [Header("Perks")]
    public PerkStatus[] perkStatus;
}
