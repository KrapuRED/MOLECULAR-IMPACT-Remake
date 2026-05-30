using UnityEngine;

public class PointOfInterestButton : MonoBehaviour
{
    [SerializeField] private PointOfInterestType type;

    public void OnPointOfInterestButtonClicked()
    {
        OutDoorManager.Instance.OnPointOfInterestButtonClicked(type);
    }
}
