using UnityEngine;

public class OutDoorManager : MonoBehaviour
{
    public static OutDoorManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);


    }

    private void Start()
    {
        StatusManager.instance.UpdateUI();
        CurrencyManager.instance.UpdateUI();
        DayCycleManager.instance.UpdateUI();
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
                SceneController.instance.BarScene();
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
