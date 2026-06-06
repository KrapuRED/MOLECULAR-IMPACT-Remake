using UnityEngine;
using UnityEngine.UI;
public class SummaryButtonEnding : MonoBehaviour
{

    [SerializeField] private Button myButton;
    void Start()
    {

        if (myButton != null)
        {
            myButton.onClick.AddListener(EndingManager.instance.OpenSummaryButton);
        }
    }
}
