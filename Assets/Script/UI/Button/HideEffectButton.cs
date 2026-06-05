using UnityEngine;

public class HideEffectButton : MonoBehaviour
{
    public void HideEffect()
    {
        GlobalEvent.OnHidePanelEffect.Invoke();
    }
}
