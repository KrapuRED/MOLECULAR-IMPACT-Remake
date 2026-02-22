using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CancelSelectedActivityEventSO", menuName = "Events/Cancel Selected Activity EventSO")]
public class CancelSelectedActivityEventSO : ScriptableObject
{
    public UnityAction<ActivitySO> OnCancelSelectedActivity;

    public void OnRaise(ActivitySO activitData)
    {
        OnCancelSelectedActivity?.Invoke(activitData);
    }

    public void Register(UnityAction<ActivitySO> listener)
    {
        OnCancelSelectedActivity += listener;
    }

    public void Unregister(UnityAction<ActivitySO> listener)
    {
        OnCancelSelectedActivity -= listener;
    }
}
