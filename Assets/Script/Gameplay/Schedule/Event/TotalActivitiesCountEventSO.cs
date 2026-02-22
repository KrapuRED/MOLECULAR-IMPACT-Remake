using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TotalActivitiesCountEventSO", menuName = "Events/Total Activities Count")]
public class TotalActivitiesCountEventSO : ScriptableObject
{
    public UnityAction<int, int> OnTotalActivitiesCount;

    public void OnRaise(int currentActivities, int maxActivities)
    {
        OnTotalActivitiesCount?.Invoke(currentActivities, maxActivities);
    }

    public void Rigester (UnityAction<int, int> listener)
    {
        OnTotalActivitiesCount += listener;
    }

    public void Unrigester(UnityAction<int, int> listener)
    {
        OnTotalActivitiesCount -= listener;
    }
}
