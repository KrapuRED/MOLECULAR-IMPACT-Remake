using UnityEngine;

public class OutDoorManager : MonoBehaviour
{
    public static OutDoorManager Instance { get; private set; }

    private bool _uiBeenRefreshed = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);


    }

    private void Start()
    {
        if (!_uiBeenRefreshed)
        {
            StatusManager.instance.UpdateUI();
        }
    }

    public void OnPointOfInterestButtonClicked(PointOfInterestType type)
    {
        switch (type)
        {
            case PointOfInterestType.Home:
                // Handle Home button click
                DayCycleManager.instance.NextDay();
                Debug.Log("Home button clicked");
                break;
            case PointOfInterestType.Library:
                // Handle Library button click
                Debug.Log("Library button clicked");
                break;
            case PointOfInterestType.Bar:
                // Handle Bar button click
                Debug.Log("Bar button clicked");
                break;
            case PointOfInterestType.Gym:
                // Handle Gym button click
                Debug.Log("Gym button clicked");
                break;
            default:
                Debug.LogWarning("Unknown point of interest type: " + type);
                break;
        }
    }
}
