using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UpdateStatusNonCurrencyEventSO", menuName = "Events/Update Status Non-Currency EventSO")]
public class UpdateStatusNonCurrencyEventSO : ScriptableObject
{
    public UnityAction<StatusSO, float> updateStatusNonCurrency;

    public void OnRaise(StatusSO statusData, float valueStatus)
    {
        updateStatusNonCurrency?.Invoke(statusData, valueStatus);
    }

    public void Register(UnityAction<StatusSO, float> listener)
    {
        updateStatusNonCurrency += listener;
    }
    public void Unregister(UnityAction<StatusSO, float> listener)
    {
        updateStatusNonCurrency -= listener;
    }
}
