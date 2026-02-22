using UnityEngine;
using TMPro;

public class TotalSelectedActivitiesUI : MonoBehaviour
{
    public TextMeshProUGUI totalSelectedActivitiesText;
    public TotalActivitiesCountEventSO totalActivitiesCountEvent;

    public void UpdateSelectedActivitiesUI(int currentActivities, int maxActivities)
    {
        totalSelectedActivitiesText.text = $"{currentActivities}/{maxActivities}";
    }

    private void OnEnable()
    {
        totalActivitiesCountEvent.Rigester(UpdateSelectedActivitiesUI);
    }

    private void OnDisable()
    {
        totalActivitiesCountEvent.Unrigester(UpdateSelectedActivitiesUI);

    }
}
