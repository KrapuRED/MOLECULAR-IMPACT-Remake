using System.Collections;
using UnityEngine;

public class EndingManager : MonoBehaviour
{

    public static EndingManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    [SerializeField] private ListOfDialogueTrigger listOfDialogueTrigger;
    [SerializeField] private GameObject summaryPanel;
    [SerializeField] private EndingCalculation endingCalculation;

    private void Start()
    {
        StartCoroutine(delay());
    }

    public void EndingCutsceneTrigger()
    {
        listOfDialogueTrigger.TriggerEndingDialogueList(endingCalculation.CheckEnding());
        Debug.Log("Tes Start");
    }

    IEnumerator delay()
    {
        yield return null;
        EndingCutsceneTrigger();
    }

    public void OpenSummaryButton()
    {
        if (summaryPanel.activeSelf) return;
        summaryPanel.SetActive(true);
    }

}
