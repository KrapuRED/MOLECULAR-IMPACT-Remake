using UnityEngine;

public enum PointOfInterestType
{
    Home,
    Gym,
    Library,
    Bar,
    Other
}

[CreateAssetMenu(fileName = "PointOfInterestSO", menuName = "Point of Interest/PointOfInterestSO")]
public class PointOfInterestSO : ScriptableObject
{
    public string PointOfInterestName;
    public PointOfInterestType PointOfInterestType;
    public Sprite PointofInterestIcon;
}
