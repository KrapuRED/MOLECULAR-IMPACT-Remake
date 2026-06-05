using System;
using UnityEngine;

public class PanelShowEffects : Panel
{
    [SerializeField] private GameObject effectButton;

    [Header("== Panel Content ==")]
    [SerializeField] private GameObject preafabEffectContent;
    [SerializeField] private Transform effectContainer;
    
    private void OnEnable()
    {
        GlobalEvent.OnShowPanelEffect.Addistener(Show);
        GlobalEvent.OnHidePanelEffect.Addistener(Hide);
    }

    private void OnDisable()
    {
        RemeveListerner();
    }

    private void OnDestroy()
    {
        RemeveListerner();
    }

    private void RemeveListerner()
    {
        GlobalEvent.OnShowPanelEffect.Removeistener(Show);
        GlobalEvent.OnHidePanelEffect.Removeistener(Hide);
    }
    
    public override void Show(string charID, object args2 = null)
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        if (effectButton == null)
        {
            Debug.Log("The Effect Button GameObject is Null");
            return;
        }
        effectButton.SetActive(false);
    }

    public override void Hide()
    {
        if (this == null)
            return;
        
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        
        if (effectButton == null)
        {
            Debug.Log("The Effect Button GameObject is Null");
            return;
        }
        effectButton.SetActive(true);
    }

    private void GenerateContent()
    {
        GameObject newContent = Instantiate(effectButton, effectContainer);
        newContent.name = "Effect Content";
    }
}
