using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Update Status Currency EventSO", menuName = "Events/Update Status Currency EventSO")]
public class UpdateStatusCurrencyEventSO : ScriptableObject
{
    public UnityAction<float> UpdateStatusCurrencyEvent;

    public void OnRiase (float currenyValue)
    {
        UpdateStatusCurrencyEvent?.Invoke(currenyValue);
    }

    public void Register(UnityAction<float> listener)
    {
        UpdateStatusCurrencyEvent += listener;
    }

    public void Unregister(UnityAction<float> listener)
    {
        UpdateStatusCurrencyEvent -= listener;
    }
}
