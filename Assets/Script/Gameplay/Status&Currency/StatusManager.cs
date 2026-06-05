using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class StatusData
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
    private List<StatusData> _status = new List<StatusData>();
    public List<StatusData> PlayerStatuses => _status;

    [Header("Events")]
    [SerializeField] private UpdateStatusNonCurrencyEventSO _updateStatusNonCurrencyUI;
    [SerializeField] private RefreshStatusUIEventSO _refreshStatusUI;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        _refreshStatusUI.Register(UpdateUI);

        GlobalEvent.OnRefreshUI.Addistener(UpdateUI);
        GlobalEvent.OnApplyRandomDayEvent.Addistener(CalculateStatusAfterRandomEffent);
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void RemoveListeners()
    {
        _refreshStatusUI.Unregister(UpdateUI);

        GlobalEvent.OnRefreshUI.Removeistener(UpdateUI);

        GlobalEvent.OnApplyRandomDayEvent.Removeistener(CalculateStatusAfterRandomEffent);
    }

    public void UpdateUI()
    {
        foreach (var status in _status)
        {
            _updateStatusNonCurrencyUI.OnRaise(status.statusData, status.statusValue);
        }
    }

    private bool CheckStatusPlayer(string statusID)
    {
        foreach (var status in _status)
        {
            if (status.statusData.statusID == statusID)
            {
                return true;
            }
        }

        return false;
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
                    CurrencyManager.instance.UpdateMoneyBoosterCurrency(benefit.valueBenefit);
            }
            else
            {
                Debug.LogWarning($"Status {benefit.statusData.name} not found in player status list.");
            }
        }
    }

    public void CalculateStatusAfterRandomEffent(string statusID, float impactValue)
    {
        if (this == null) return;

        if (!CheckStatusPlayer(statusID))
        {
            Debug.LogWarning($"Status with ID {statusID} not found in player status list.");
            return;
        }

        var status = _status.Find(s => s.statusData.statusID == statusID);

        if (status == null)
        {
            Debug.LogError($"Cannot Find StatusSO in in player status list!");
            return;
        }

        status.statusValue += impactValue;
        UpdateUI();
    }

    //public List<StatusData> ReturnStatusPlayer()
    //{
    //    return _status;
    //}

}
