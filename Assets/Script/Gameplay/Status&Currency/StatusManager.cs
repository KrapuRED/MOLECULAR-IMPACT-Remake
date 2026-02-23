using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerStatus
{
    public StatusSO statusData;
    [Range(0, 100)]
    public float statusValue;
}

public class StatusManager : MonoBehaviour
{
    public static StatusManager instance;

    [Header("Status Config")]
    [SerializeField]
    private List<PlayerStatus> _status = new List<PlayerStatus>();

    [Header("Events")]
    [SerializeField] private UpdateStatusNonCurrencyEventSO _updateStatusNonCurrencyUI;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    //TO CALCULATE EFFECTS FOR STATUS AND CURRENCY
    public void CalculatBenefitToStatus(ActivitySO activity)
    {
       foreach (var benefit in activity.benefits)
        {
            var statusBenefit = _status.Find(s => s.statusData.statusID == benefit.statusData.statusID);

            if (statusBenefit != null)
            {
                if (benefit.statusData.statusType == StatusType.Non_Currency)
                    statusBenefit.statusValue += benefit.valueBenefit;
                else
                    CurrencyManager.instance.UpdateCurrency(benefit.valueBenefit);
            }
            else
            {
                Debug.LogWarning($"Status {benefit.statusData.name} not found in player status list.");
            }
        }
    }

    public void UpdateUI()
    {
        foreach (var status in _status)
        {
            _updateStatusNonCurrencyUI.OnRaise(status.statusData, status.statusValue);
        }
    }
}
