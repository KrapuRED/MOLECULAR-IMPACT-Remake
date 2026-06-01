using UnityEngine;
using System.Collections.Generic;
using System;

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
        CommitmentData commitmentData = commitments.Find(c => c.characterID == charID);

        if (commitmentData == null)
        {
            Debug.LogWarning($"[CommitmentManager] No commitmentData data found for charID: {charID}");
            return false;
        }

        // 2) Check interaction count via GameStateManager
        int interactionCount = GameStateManager.instance.GetInteractionCount(charID);

        if (interactionCount < commitmentData.minimumInteraction)
        {
            Debug.Log($"[CommitmentManager] '{commitmentData.commitmentName}' failed: " +
                      $"needs {commitmentData.minimumInteraction} interactions, has {interactionCount}.");
            return false;
        }

        Debug.Log($"[CommitmentManager] '{commitmentData.commitmentName}' commitmentData unlocked for {charID}!");
        return true;
    }

    /// <summary>
    /// Checks whether the player has all perks required for this commitmentData.
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

    public void OnHasCommitment(string charID, int energyConsume)
    {
        // Player has met the requirements for a commitmentData, so we can add it to the active list and apply its effects

        // 1) Find the matching CommitmentData for this character
        CommitmentData commitment = commitments.Find(c => c.characterID == charID);

        // 2) Check all required perks are active via PerkManager
        if (commitment == null)
            return;

        if (!HasAllRequiredPerks(commitment))
        {
            GlobalEvent.OnShowPanelNoMeetRequirment.Invoke(charID, commitment);
            return;
        }

        CurrencyManager.instance.ConsumeEnergy(energyConsume);

        Debug.Log("[CommitmentManager] Player has met commitmentData requirements. Applying effects...");
        if (commitment.changeActivitys.Count <= 0)
        {
            Debug.LogWarning($"[CommitmentManager] No effects to apply for charID: {charID}");
            return;
        }
        
        Debug.Log($"[CommitmentManager] Applying {commitment.changeActivitys.Count} activity changes for commitmentData: {commitment.commitmentName}");
        foreach (var effect in commitment.changeActivitys)
        {
            Debug.Log($"[CommitmentManager] Applying effect of activity: {effect.activityName}");
        }
    }
}
