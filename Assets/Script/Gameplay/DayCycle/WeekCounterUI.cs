using UnityEngine;
using TMPro;

public class WeekCounterUI : MonoBehaviour
{
    public TextMeshProUGUI weekCounertUIText;

    private void OnEnable()
    {
        GlobalEvent.OnUpdateWeekCounertUI.Addistener(UpdateWeekCounertUI);
    }

    private void OnDisable()
    {
        RemoveListener();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }

    private void RemoveListener()
    {
        GlobalEvent.OnUpdateWeekCounertUI.Removeistener(UpdateWeekCounertUI);
    }

    private void UpdateWeekCounertUI(int weekCount, int maxWeek)
    {
        weekCounertUIText.text = $"{weekCount} / {maxWeek} weeks";
    }
}
