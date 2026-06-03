using System.Collections.Generic;
using UnityEngine;

public class ListOfDialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<DialogueTrigger> dialogueTriggers;
    private int maxDialogueTrigger;
    private int currDialogueTrigger = 0;

    private void Awake()
    {
        int dialogueTriggerChild = dialogueTriggers.Count;
        maxDialogueTrigger = dialogueTriggerChild;
    }

    public void TriggerDialogueList()
    {
        Debug.Log($"currDialogueTrigger = {currDialogueTrigger}");
        dialogueTriggers[currDialogueTrigger].TriggerDialogue();
        if (currDialogueTrigger >= maxDialogueTrigger - 1)
        {
            currDialogueTrigger = maxDialogueTrigger - 1;
        }
        else
        {
            currDialogueTrigger++;
        }
           
    }

    public void TriggerEndingDialogueList(int index)
    {
        Debug.Log($"currDialogueTrigger = {index}");
        dialogueTriggers[index].TriggerDialogue();
        //if (currDialogueTrigger >= maxDialogueTrigger - 1)
        //{
        //    currDialogueTrigger = maxDialogueTrigger - 1;
        //}
        //else
        //{
        //    currDialogueTrigger++;
        //}

    }
}
