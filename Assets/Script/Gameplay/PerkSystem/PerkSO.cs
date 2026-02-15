using UnityEngine;

public enum PerkType
{
    none,
    good,
    bad,
    commitment
}

public enum PerkDuration
{
    none,
    Permanent,
    Temporary
}

[System.Serializable]
public class PerkEffect
{
    public string effectName;
    public int effectValue;
}


[CreateAssetMenu(fileName = "PerkData", menuName = "Game Data/PerkSO")]
public class PerkSO : ScriptableObject
{
    public string perkID;
    public string perkName;
    [TextArea(10, 15)]
    public string perkDescription;

    public int perkRequirement;
    public PerkType perkType;
    public PerkDuration perkDuration;
    public int longDuration;

    public PerkEffect[] perkEffect;
}
