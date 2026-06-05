using System;
using UnityEngine;
using System.Collections.Generic;

public class PanelShowEffects : Panel
{
    [SerializeField] private GameObject effectButton;

    [Header("== Panel Content ==")]
    [SerializeField] private GameObject prefabEffectContent;
    [SerializeField] private Transform effectContainer;
    
    private void OnEnable()
    {
        GlobalEvent.OnShowPanelEffect.Addistener(Show);
        GlobalEvent.OnHidePanelEffect.Addistener(Hide);
        GlobalEvent.OnUpdatePerkUI.Addistener(GenerateContent);
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

        GlobalEvent.OnUpdatePerkUI.Removeistener(GenerateContent);
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

    private void GenerateContent(List<PerkSO> perkDatas)
    {
        for (int i = 0; i < perkDatas.Count; i++)
        {
            GameObject newContent = Instantiate(prefabEffectContent, effectContainer);
            newContent.name = "Effect Content " + perkDatas[i].perkName;

            ShowEffectsContentUI contentUI = newContent.GetComponent<ShowEffectsContentUI>();
            if (contentUI == null)
            {
                Debug.LogWarning("The ShowEffectsContentUI Component is Null");
                Destroy(newContent);
                continue;
            }

            contentUI.IntializeEffects(perkDatas[i]);
        }
    }
}
