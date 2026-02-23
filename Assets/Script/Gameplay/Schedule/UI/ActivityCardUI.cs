using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivityCardUI : MonoBehaviour
{
    [SerializeField] private ActivitySO _activityData;

    [Header("Benefit Config UI")]
    public Transform containerBenefitUI;
    [SerializeField] private ActivityCardBenefitUI[] _benefits;
    

    [Header("Reference")]
    public TextMeshProUGUI nameActivityText;
    public Image iconActivityImg;

    public ActivitySO activityData => _activityData;

    private void Awake()
    {
        if (containerBenefitUI != null)
            _benefits = containerBenefitUI.GetComponentsInChildren<ActivityCardBenefitUI>(true);
    }

    public void SetCardActivity(ActivitySO data)
    {
        if (_activityData == null)
            _activityData = data;

        nameActivityText.text = data.activityName;
        //set icon
        iconActivityImg.sprite = data.activityIcon;
        SetUpBenefits();
    }

    private void SetUpBenefits()
    {
        if (_benefits.Length <= 0 || _activityData.benefits.Length <= 0)
        {
            Debug.Log($"Something missing either on benefits or _activityData benefits because is below 0");
            return;
        }

        for (int i = 0; i < _activityData.benefits.Length; i++)
        {
            _benefits[i].gameObject.SetActive(true);
            _benefits[i].SetActivityCardBenefitUI(_activityData.benefits[i].status.ToString(),
                                                  _activityData.benefits[i].valueBenefit.ToString());
        }
    }
}
