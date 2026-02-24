using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RefreshStatusUIEventSO", menuName = "Global Events/Refresh StatusUI EventSO")]
public class RefreshStatusUIEventSO : ScriptableObject
{
    public UnityEvent RefreshStatusUI;

    public void OnRiase()
    {
        RefreshStatusUI?.Invoke();
    }

    public void Register(UnityAction listener)
    {
        RefreshStatusUI.AddListener(listener);
    }

    public void Unregister(UnityAction listener)
    {
        RefreshStatusUI.RemoveListener(listener);
    }
}
