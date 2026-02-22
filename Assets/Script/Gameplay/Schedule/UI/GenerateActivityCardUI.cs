using UnityEngine;

public class GenerateActivityCardUI : MonoBehaviour
{
    [Header("Generate Activity Card Config")]
    public GameObject prefabActivityCard;

    public void GenerateActivityCard(ActivitySO cardData, Transform container)
    {
        GameObject newCard = Instantiate(prefabActivityCard, container);

        ActivityCardUI activityCardUI = newCard.GetComponent<ActivityCardUI>();
        
        if (activityCardUI == null )
        {
            Debug.LogError("The prefabActivityCard is not have ActivityCardUI");
            return;
        }

        activityCardUI.SetCardActivity(cardData);
    }
}
