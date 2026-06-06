using UnityEngine;

public class NotificationUI : MonoBehaviour
{

    public void SetUpNotification(PerkSO perkData)
    {
        // Set up the notification UI based on the perk data
        // For example, you can set the text, image, etc. based on the perkData
        Debug.Log("NotificationUI SetUpNotification called with perk: " + perkData.perkName);
    }
}
