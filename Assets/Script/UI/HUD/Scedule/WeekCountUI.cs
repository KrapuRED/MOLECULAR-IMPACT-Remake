using TMPro;
using UnityEngine;

public class WeekCountUI : MonoBehaviour
{
    public TextMeshProUGUI weekCountUIText;

    public UpdateWeekCountEventSO UpdateWeekCountEvent;

    private void UpdateWeekCountUIText(int weekCount)
    {
        weekCountUIText.text = "Week " + weekCount;
    }

    //Event
    private void OnEnable()
    {
        UpdateWeekCountEvent.Register(UpdateWeekCountUIText);  
    }

    private void OnDisable()
    {
        UpdateWeekCountEvent.Unregister(UpdateWeekCountUIText);
    }
}
