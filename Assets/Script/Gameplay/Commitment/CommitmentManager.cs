using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class ChangeActivity
{
    public ActivitySO RemoveActivity;
    public ActivitySO ReplaceActivity;
}

[System.Serializable]
public class CommitmentData
{
    public string commitmentName;
    public string characterID;
    public int minimumInteraction;
    public List<PerkSO> requirmentPerks = new();
    
    [Header("Effects on Status")]
    public List<StatusMultiplier> statusMultipliers = new();

    [Header("Effects on Activities")]
    public List<ChangeActivity> changeActivitys = new();
    public List<ActivityMultipliers> activityMultipliers = new();
}

public class CommitmentManager : MonoBehaviour
{
    public static CommitmentManager Instance { get; private set; }

    [SerializeField] private List<CommitmentData> commitments = new List<CommitmentData>();
    [SerializeField] private List<CommitmentData> activeCommitments = new List<CommitmentData>();

    private List<ChangeActivity> _pendingActivityChanges = new();
    private ScheduleManager _scheduleManager;

    [Header("Test Config")]
    [SerializeField] private bool testCommitment;
    [SerializeField] private string testCharID;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (testCommitment)
        {
            Test(testCharID, 0);
        }
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

        if (activeCommitments.Contains(commitment))
        {
            Debug.LogWarning($"[CommitmentManager] '{commitment.commitmentName}' already active.");
            return;
        }

        CurrencyManager.instance.ConsumeEnergy(energyConsume);

        if (commitment.changeActivitys.Count <= 0)
        {
            Debug.LogWarning($"[CommitmentManager] No effects to apply for charID: {charID}");
            return;
        }

        // 3) Apply activityMulti changes via ActivityManager
        foreach (var activityMulti in commitment.activityMultipliers)
        {
            ActivityManager.instance.SetMultiplierByActivityCategory(activityMulti.activityCategory, activityMulti.multiplierValue);
        }

        // 4) Apply status multipliers via StatusManager
        foreach (var statusMult in commitment.statusMultipliers)
        {
            Debug.Log($"[CommitmentManager] Applying status multiplier: {statusMult.statusData.name} x{statusMult.multiplierValue}");
            StatusManager.instance.OnSetMultiplierByCommitment(statusMult.statusData.statusID, statusMult.multiplierValue);
        }

        // 5) Apply activity changes via ScheduleManager
        if (commitment.changeActivitys.Count > 0)
        {
            if (_scheduleManager != null)
            {
                _scheduleManager.ApplyCommitmentActivities(commitment.changeActivitys);
            }
            else
            {
                Debug.LogWarning("[CommitmentManager] ScheduleManager not registered — " +
                                 "activity changes will apply on next scene load.");
                // Store pending changes to apply when ScheduleManager registers
                _pendingActivityChanges.AddRange(commitment.changeActivitys);
            }
        }

        // 6) Add this commitmentData to the active commitments list
        activeCommitments.Add(commitment);
    }

    public void RegisterScheduleManager(ScheduleManager sm)
    {
        _scheduleManager = sm;

        // Flush any commitment activity changes that happened before this scene loaded
        if (_pendingActivityChanges.Count > 0)
        {
            
            _scheduleManager.ApplyCommitmentActivities(_pendingActivityChanges);
            _pendingActivityChanges.Clear();
        }
    }

    void Test(string charID, int energyConsume)
    {
        CommitmentData commitment = commitments.Find(c => c.characterID == charID);

        // 2) Check all required perks are active via PerkManager
        if (commitment == null)
            return;

        CurrencyManager.instance.ConsumeEnergy(energyConsume);

        if (commitment.changeActivitys.Count <= 0)
        {
            Debug.LogWarning($"[CommitmentManager] No effects to apply for charID: {charID}");
            return;
        }

        // 3) Apply activityMulti changes via ActivityManager
        foreach (var activityMulti in commitment.activityMultipliers)
        {
            ActivityManager.instance.SetMultiplierByActivityCategory(activityMulti.activityCategory, activityMulti.multiplierValue);
        }

        // 4) Apply status multipliers via StatusManager
        foreach (var statusMult in commitment.statusMultipliers)
        {
            Debug.Log($"[CommitmentManager] Applying status multiplier: {statusMult.statusData.name} x{statusMult.multiplierValue}");
            StatusManager.instance.OnSetMultiplierByCommitment(statusMult.statusData.statusID, statusMult.multiplierValue);
        }

        // 5) Apply activity changes via ScheduleManager
        if (commitment.changeActivitys.Count > 0)
        {
            if (_scheduleManager != null)
            {
                _scheduleManager.ApplyCommitmentActivities(commitment.changeActivitys);
            }
            else
            {
                Debug.LogWarning("[CommitmentManager] ScheduleManager not registered — " +
                                 "activity changes will apply on next scene load.");
                // Store pending changes to apply when ScheduleManager registers
                _pendingActivityChanges.AddRange(commitment.changeActivitys);
            }
        }

        // 6) Add this commitmentData to the active commitments list
        activeCommitments.Add(commitment);
    }
}
