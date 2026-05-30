using UnityEngine;


public class PointOfInterestBuilding : PointOfInterest
{
    [SerializeField] private PointOfInterestSO pointOfInterestSO;

    [SerializeField] private PointOfInterestBuildingIcon pointOfInterestBuildingIcon;

    private void Start()
    {
        SetPointofInterest();
    }

    public void SetPointofInterest()
    {
        if (pointOfInterestSO == null)
        {
            Debug.LogError("pointOfInterestSO is null. Cannot set point of interest.");
            return;
        }

        if (pointOfInterestBuildingIcon == null)
        {
            Debug.LogError("pointOfInterestBuildingIcon is null. Cannot set point of interest icon.");
            return;
        }

        pointOfInterestBuildingIcon.SetIcon(pointOfInterestSO.PointofInterestIcon);
    }
}
