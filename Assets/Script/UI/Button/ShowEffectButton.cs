using UnityEngine;

public class ShowEffectButton : MonoBehaviour
{
    public void ShowEffect()
    {
        GlobalEvent.OnShowPanelEffect.Invoke(string.Empty, null);
    }
}
