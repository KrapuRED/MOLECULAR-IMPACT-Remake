using UnityEngine;
using UnityEngine.UI;

public class PointOfInterestBuildingIcon : MonoBehaviour
{
    [SerializeField] private Image pointOfInterestBuildingIcon;

    public void SetIcon(Sprite pointofInterestIcon)
    {
        if (this == null)

        if (pointofInterestIcon == null)
        {
            Debug.LogError("pointofInterestIcon is null. Cannot set icon.");
            return;
        }

        if (pointOfInterestBuildingIcon == null)
            pointOfInterestBuildingIcon = GetComponent<Image>();

        pointOfInterestBuildingIcon.sprite = pointofInterestIcon;
    }
}
