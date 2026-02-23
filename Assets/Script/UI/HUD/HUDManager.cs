using UnityEngine;
using System.Collections.Generic;

public enum PanelType
{
    None,
    SchedulePanel
}

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

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void OpenPanel(string panelID)
    {
        //pause ignore isPanelOpen

        if (_isPanelOpened)
            return;

        for (int i = 0; i < panels.Count; i++)
        {
            if (panels[i].hudPanel.panelID == panelID)
            {
                _isPanelOpened = true;
                panels[i].isPanelActive = true;
                panels[i].hudPanel.ShowPanel(_isPanelOpened);
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
            panel.hudPanel.HidePanel(_isPanelOpened);
        }
    }

    private void RefreshUI()
    {
        DayCycleManager.instance.UpdateUI();
        StatusManager.instance.UpdateUI();
    }
}
