using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CommitmentData
{
    public string commitmentName;
    public string characterID;
    public int minimumInteraction;
    public List<PerkSO> requirmentPerks = new();
    public List<ActivitySO> changeActivitys = new();
}

public class CommitmentManager : MonoBehaviour
{
    public static CommitmentManager Instance { get; private set; }

    [SerializeField] private List<CommitmentData> commitments = new List<CommitmentData>();
    [SerializeField] private List<CommitmentData> activeCommitments = new List<CommitmentData>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool CommitmentCharacterByCharID(string charID)
    {
        // 1) Find the matching CommitmentData for this character
        CommitmentData commitment = commitments.Find(c => c.characterID == charID);

        if (commitment == null)
        {
            Debug.LogWarning($"[CommitmentManager] No commitment data found for charID: {charID}");
            return false;
        }

        // 2) Check interaction count via GameStateManager
        int interactionCount = GameStateManager.instance.GetInteractionCount(charID);

        if (interactionCount < commitment.minimumInteraction)
        {
            Debug.Log($"[CommitmentManager] '{commitment.commitmentName}' failed: " +
                      $"needs {commitment.minimumInteraction} interactions, has {interactionCount}.");
            return false;
        }

        // 3) Check all required perks are active via PerkManager
        if (!HasAllRequiredPerks(commitment))
        {
            return false;
        }

        Debug.Log($"[CommitmentManager] '{commitment.commitmentName}' commitment unlocked for {charID}!");
        return true;
    }

    /// <summary>
    /// Checks whether the player has all perks required for this commitment.
    /// </summary>
    private bool HasAllRequiredPerks(CommitmentData commitment)
    {
        foreach (PerkSO requiredPerk in commitment.requirmentPerks)
        {
            if (requiredPerk == null)
            {
                Debug.LogWarning($"[CommitmentManager] Null PerkSO found in '{commitment.commitmentName}' requirements.");
                continue;
            }

            if (!PerkManager.instance.IsPerkActive(requiredPerk.perkID))
            {
                Debug.Log($"[CommitmentManager] '{commitment.commitmentName}' failed: " +
                          $"missing required perk '{requiredPerk.perkName}'.");
                return false;
            }
        }

        return true;
    }
}
