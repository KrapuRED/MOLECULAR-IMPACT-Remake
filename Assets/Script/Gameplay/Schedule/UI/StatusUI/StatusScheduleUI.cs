using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusScheduleUI : StatusUI
{
    [SerializeField] private string _statusID;
    [SerializeField] private TextMeshProUGUI nameStatusText;
    [SerializeField] private TextMeshProUGUI valueStatusText;
    [SerializeField] private Image iconStatusText;

    public override void UpdateStatusUI(StatusSO statusData, float valueStatus)
    {
        if (statusData.statusID == _statusID)
        {
            nameStatusText.text = statusData.statusName;
            valueStatusText.text = $"{valueStatus}";
        }
    }
}
