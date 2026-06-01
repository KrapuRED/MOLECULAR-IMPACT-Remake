using UnityEngine;

public class CommitmentButton : MonoBehaviour
{
    [SerializeField] private int consume;

    [SerializeField] private CharacterDataSO charData;
    [SerializeField] private CanvasGroup canvasGroup;

    private CommitmentManager _commitmentManager;

    private void Start()
    {
        _commitmentManager = CommitmentManager.Instance;
    }

    private void OnEnable()
    {
        GlobalEvent.OnShowCommitmentBotton.Addistener(InitializedCommitmentButton);
        GlobalEvent.OnHideCommitmentBotton.Addistener(HideCommitmentButton);
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
       GlobalEvent.OnShowCommitmentBotton.Removeistener(InitializedCommitmentButton);
       GlobalEvent.OnHideCommitmentBotton.Removeistener(HideCommitmentButton);
    }

    private void InitializedCommitmentButton()
    {
        if (this == null) return; // Check if the GameObject has been destroyed

        if (_commitmentManager == null)
        {
            Debug.LogError("[CommitmentButton] No CommitmentManager instance found in the scene.");
            return;
        }

        if (charData == null)
        {
            Debug.LogError("[CommitmentButton] No CharacterDataSO assigned to the CommitmentButton.");
            return;
        }

        bool isUnlocked = _commitmentManager.CommitmentCharacterByCharID(charData.characterID);

        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();  

        if (isUnlocked)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void HideCommitmentButton()
    {
        if (this == null) return; // Check if the GameObject has been destroyed

        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnCommitmentButtonClick()
    {
        // Consume Energy
        _commitmentManager.OnHasCommitment(charData.characterID, consume);
    }
}
