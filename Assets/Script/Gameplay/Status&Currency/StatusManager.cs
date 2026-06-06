using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class StatusMultiplier
{
    public StatusSO statusData;
    public float multiplierValue;
}

[System.Serializable]
public class StatusData
{
    public StatusSO statusData;
    [Range(0, 100)]
    public float statusValue;
    public float multiplierValue = 1;
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
    public void CalculatBenefitToStatus(ActivitySO activity, float activityMultiplier = 1f)
    {
        foreach (var benefit in activity.benefits)
        {
            var statusBenefit = _status.Find(s => s.statusData.statusID == benefit.statusData.statusID);

            if (statusBenefit != null)
            {
                if (benefit.statusData.statusType == StatusType.Non_Currency)
                {
                    float finalBenefitValue = benefit.valueBenefit
                        * (statusBenefit.multiplierValue / 100f)
                        * activityMultiplier; // <- category multiplier from commitment

                    statusBenefit.statusValue += finalBenefitValue;
                }
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

    public void OnSetMultiplierByCommitment(string statusID, float multiplierValue)
    {
        var statusData = _status.Find(s => s.statusData.statusID == statusID);

        if (statusData != null)
        {
            statusData.multiplierValue += multiplierValue;
            Debug.Log($"[StatusManager] Multiplier for status '{statusData.statusData.name}' set to {multiplierValue} due to commitment.");
        }
        else
        {
            Debug.LogWarning($"[StatusManager] Status with ID '{statusID}' not found when trying to set multiplier.");
        }
    }

    //public List<StatusData> ReturnStatusPlayer()
    //{
    //    return _status;
    //}

}
