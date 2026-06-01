using System.Collections;
using UnityEngine;

public class EndingManager : MonoBehaviour
{

    [SerializeField] private ListOfDialogueTrigger listOfDialogueTrigger;


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

}
