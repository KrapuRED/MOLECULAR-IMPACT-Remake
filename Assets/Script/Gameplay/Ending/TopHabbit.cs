using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TopHabbit : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text topHabbit;
    [SerializeField] private TMP_Text nameHabbit;
    [SerializeField] private TMP_Text countText;

    public void SetUp(int top, EndingActivityData data)
    {
        icon.sprite = data.activitySO.activityIcon;

        topHabbit.text = "#" + top.ToString();

        nameHabbit.text = data.activitySO.activityName;

        countText.text = data.activityCount.ToString();
    }
}
