using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class StatusBedRoomUI : StatusUI
{
    [SerializeField] private string _statusID;
    [SerializeField] private TextMeshProUGUI valueStatusText;
    [SerializeField] private Image iconStatusText;

    public override void UpdateStatusUI(StatusSO statusData, float valueStatus)
    {
        if (statusData.statusID == _statusID)
        {
            valueStatusText.text = $"{valueStatus}";
        }
    }
}
