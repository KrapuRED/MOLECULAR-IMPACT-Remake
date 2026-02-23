using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField] private string _statusID;
    [SerializeField] private TextMeshProUGUI nameStatusText;
    [SerializeField] private TextMeshProUGUI valueStatusText;
    [SerializeField] private Image iconStatusText;

    private void UpdateStatusUI(string statusID, string statusName, int valueStatus)
    {

    }
}
