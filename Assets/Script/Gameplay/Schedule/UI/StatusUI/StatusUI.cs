using UnityEngine;

public class StatusUI : MonoBehaviour
{

    public UpdateStatusNonCurrencyEventSO updateStatusNonCurrencyEvent;

    public virtual void UpdateStatusUI(StatusSO statusData, float valueStatus)
    {

    }

    private void OnEnable()
    {
        updateStatusNonCurrencyEvent.Register(UpdateStatusUI);
    }

    private void OnDisable()
    {
        updateStatusNonCurrencyEvent.Unregister(UpdateStatusUI);
    }
}
