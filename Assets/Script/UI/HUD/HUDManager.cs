using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Panel
{
    public HUD hudPanel;
    public bool isPanelActive;
}



public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [SerializeField] private List<Panel> panels = new List<Panel>();

    [SerializeField] private bool _isPanelOpened;

    [Header("Events")]
    [SerializeField] private RefreshStatusUIEventSO _refreshStatusUI;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }



    public void OpenPanel(string panelID)
    {
        //pause ignore isPanelOpen
        if (panels[0].hudPanel.panelID == panelID)
        {
            panels[0].isPanelActive = true;
            panels[0].hudPanel.ShowPanel(panels[0].isPanelActive);
            return;
        }

        if (_isPanelOpened)
            return;

        for (int i = 0; i < panels.Count; i++)
        {
            if (panels[i].hudPanel.panelID == panelID)
            {
                _isPanelOpened = true;
                panels[i].isPanelActive = true;
                panels[i].hudPanel.ShowPanel(_isPanelOpened);
                break;
            }
        }

        RefreshUI();
    }

    public void ClosePanel()
    {
        //if puase panel active, then deactived the puasePanel first then click close panel


        _isPanelOpened = false;
        foreach (Panel panel in panels)
        {
            if (!panel.isPanelActive)
                continue;

            panel.isPanelActive = false;
            panel.hudPanel.HidePanel();
        }
    }

    private void RefreshUI()
    {
        _refreshStatusUI.OnRiase();
    }
}
