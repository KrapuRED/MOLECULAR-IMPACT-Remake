using UnityEngine;

public class GenerateSelectedActivityCardUI : MonoBehaviour
{
    public GameObject prefabSelectedActivityCard;

    public void GenerateSelectedActivityCard(ActivitySO activityData, Transform container)
    {
        GameObject newSelectedActivityUI = Instantiate(prefabSelectedActivityCard, container);

        IconSelectedActivityUI iconSelectedActivityUI = newSelectedActivityUI.GetComponent<IconSelectedActivityUI>();
        if (iconSelectedActivityUI == null)
        {
            Debug.LogError($"new selected Activity UI is missing script iconSelectedActivityUI!");
            return;
        }

        iconSelectedActivityUI.SetSelectedActivityUI(activityData);
    }
}
