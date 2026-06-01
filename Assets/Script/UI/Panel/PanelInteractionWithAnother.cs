using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelInteractionWithAnother : Panel
{
    [SerializeField] private string eventID;
    [SerializeField] private EventOutDoorSO eventOutDoorData;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonParent;

    private HashSet<string> eventOutDoorDataBeenGenerates = new();

    private void OnEnable()
    {
        GlobalEvent.OnShowPanelInteractionAnother.Addistener(Show);
        GlobalEvent.OnHidePanelInteractionAnother.Addistener(Hide);
    }

    private void OnDisable()
    {
        RemoveListener();
    }
    private void OnDestroy()
    {
        RemoveListener();
    }

    private void RemoveListener()
    {
        GlobalEvent.OnShowPanelInteractionAnother.Removeistener(Show);
        GlobalEvent.OnHidePanelInteractionAnother.Removeistener(Hide);
    }

    public override void Show(string charID, object args2 = null)
    {
        if (this == null) return; // Check if the panel has been destroyed before trying to show it

        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        if (charID != eventID)
        {
            return; // If the character ID does not match, do not show the panel
        }

        // Check if the event data has already been generated for this character, if not generate it and add it to the hash set

        if (eventOutDoorData != null)
        {
            foreach (var eventData in eventOutDoorData.eventOutDoorList)
            {
                if (!eventOutDoorDataBeenGenerates.Contains(eventData.eventID))
                {
                    eventOutDoorDataBeenGenerates.Add(eventData.eventID);

                    GenerateButton(eventData);

                    Debug.Log("Succes Generate event ID: " + eventData.eventID);
                }
            }
        }

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void GenerateButton(EventOutDoor eventData)
    {
        GameObject button = Instantiate(buttonPrefab, buttonParent);
        InteractionWithAnotherButton buttonScript = button.GetComponent<InteractionWithAnotherButton>();
        buttonScript.InitializedButton(eventData);
    }

    public override void Hide()
    {
        if (this == null) return; // Check if the panel has been destroyed before trying to show it

        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        //Clear or Hide text and botton
        GlobalEvent.OnHideCommitmentBotton.Invoke();
    }
}
