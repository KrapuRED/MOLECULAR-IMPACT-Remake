using UnityEngine;

public enum ActivityName
{
    LightBookReading
}

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


[CreateAssetMenu(fileName = "activity", menuName = "activity card/activity")]
public class activitySO : ScriptableObject
{
    public ActivityName activityName;
    public ActivityType activityType;

    [System.Serializable]
    public class Benefit
    {
        public Status status;
        public int valueBenefit;
    }
    public Benefit[] benefits;
}
