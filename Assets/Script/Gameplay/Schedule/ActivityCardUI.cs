using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivityCardUI : MonoBehaviour
{
    public ActivitySO activityData;

    [Header("Benefit Config UI")]
    public Transform containerBenefitUI;
    [SerializeField] private ActivityCardBenefitUI[] _benefits;

    [Header("Reference")]
    public TextMeshProUGUI nameActivityText;
    public Image iconActivityImg;

    private void Start()
    {
        if (containerBenefitUI != null)
            _benefits = containerBenefitUI.GetComponentsInChildren<ActivityCardBenefitUI>(true);
        
        SetCardActivity(activityData);
    }

    public void SetCardActivity(ActivitySO data)
    {
        if (activityData == null)
            activityData = data;

        nameActivityText.text = data.name;
        //set icon
        iconActivityImg.sprite = data.activityIcon;
        SetUpBenefits();
    }

    private void SetUpBenefits()
    {
        if (_benefits.Length <= 0 || activityData.benefits.Length <= 0)
        {
            Debug.Log($"Something missing either on benefits or activityData benefits because is below 0");
            return;
        }

        for (int i = 0; i < activityData.benefits.Length; i++)
        {
            _benefits[i].gameObject.SetActive(true);
            _benefits[i].SetActivityCardBenefitUI(activityData.benefits[i].status.ToString(),
                                                  activityData.benefits[i].valueBenefit.ToString());
        }
    }
}
