using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Update WeekCount Event", menuName = "Events/Update WeekCount EventSO")]
public class UpdateWeekCountEventSO : ScriptableObject
{
    public UnityAction<int> OnUpdateWeekCount;

    public void RaiseEvent(int weekCount)
    {
        OnUpdateWeekCount?.Invoke(weekCount);
    }

    public void Register(UnityAction<int> listener)
    {
        OnUpdateWeekCount += listener;
    }

    public void Unregister(UnityAction<int> listener)
    {
        OnUpdateWeekCount -= listener;
    }
}
