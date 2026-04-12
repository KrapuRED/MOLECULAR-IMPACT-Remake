using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TopHabbit : MonoBehaviour
{
    public ActivitySO activitySO;

    public Image icon;
    public TMP_Text topHabbit;
    public TMP_Text nameHabbit;
    void Start()
    {

    }

    public void SetUp(int top)
    {
        icon.sprite = activitySO.activityIcon;
        topHabbit.text = top.ToString();
        nameHabbit.text = activitySO.activityName.ToString();
    }
}
