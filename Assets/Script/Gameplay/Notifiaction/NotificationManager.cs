using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;

    [SerializeField] private Transform notificationContainer;
    [SerializeField] private GameObject notificationContainerPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    public void ShowActivePerkNotification(PerkSO perkData)
    {
        GameObject newNotif = Instantiate(notificationContainerPrefab, notificationContainer);
        newNotif.GetComponent<NotificationUI>().SetUpNotification(perkData);
    }
}
