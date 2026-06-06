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

    private void Start()
    {
        StartCoroutine(delay());
    }

    public void EndingCutsceneTrigger()
    {
        listOfDialogueTrigger.TriggerEndingDialogueList(1);
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
