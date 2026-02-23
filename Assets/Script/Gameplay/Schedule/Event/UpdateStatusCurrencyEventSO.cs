using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Update Status Currency EventSO", menuName = "Events/Update Status Currency EventSO")]
public class UpdateStatusCurrencyEventSO : ScriptableObject
{
    public UnityAction<int> UpdateStatusCurrencyEvent;

    public void OnRiase (int currenyValue)
    {
        UpdateStatusCurrencyEvent?.Invoke(currenyValue);
    }

    public void Register(UnityAction<int> listener)
    {
        UpdateStatusCurrencyEvent += listener;
    }

    public void Unregister(UnityAction<int> listener)
    {
        UpdateStatusCurrencyEvent -= listener;
    }
}
