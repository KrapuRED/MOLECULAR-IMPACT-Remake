using UnityEngine;

public class HidePanelInteractionuButton : MonoBehaviour
{
    public void OnHidePanelInteractionButtonClick()
    {
        GlobalEvent.OnHidePanelInteraction.Invoke();
        GlobalEvent.OnHidePanelInteractionAnother.Invoke();
    }
}
