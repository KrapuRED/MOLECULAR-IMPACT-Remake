using TMPro;
using UnityEngine;

public class ActivityCardBenefitUI : MonoBehaviour
{
    public TextMeshProUGUI statusNameText;
    public TextMeshProUGUI statusValueText;

    public void SetActivityCardBenefitUI(string nameStatus, string statusValue)
    {
        statusNameText.text = nameStatus;
        statusValueText.text = statusValue;
    }
}
