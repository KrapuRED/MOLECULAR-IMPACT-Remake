using UnityEngine;
using System.Collections.Generic;

[System.Serializable]

public class InteractionCharacter
{
    public string characterName;
    public string characterID;
    public int interactionCount;
}

[System.Serializable]
public class GameState
{
    public List<ActivitySO> SelectedActivities = new ();
    public List<InteractionCharacter> InteractionCharacters = new ();
}
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    [SerializeField] private GameState _gameState = new ();

    public List<ActivitySO> GetSelectedActivities() => _gameState.SelectedActivities;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsInteractionCharacterExist(string characterID)
    {
        return _gameState.InteractionCharacters.Exists(character => character.characterID == characterID);
    }

    public int GetInteractionCount(string characterID)
    {
        var character = _gameState.InteractionCharacters.Find(character => character.characterID == characterID);
        return character != null ? character.interactionCount : 0;
    }

    public void SetActivityGameState(List<ActivitySO> activityDatas)
    {

        if (activityDatas.Count <= 0)
        {
            Debug.LogWarning("Trying to add null activity to GameStateManager");
            return;
        }

        _gameState.SelectedActivities = activityDatas;
    }

    public void SetInteractionDataGameState(string charName, string charID)
    {
        if (IsInteractionCharacterExist(charID))
        {
            // If the character already exists, increment the interaction count
            var character = _gameState.InteractionCharacters.Find(character => character.characterID == charID);

            if (character != null)
            {
                character.interactionCount++;
            }
        }
        else
        {
            // If the character does not exist, add a new entry to the list
            _gameState.InteractionCharacters.Add(new InteractionCharacter
            {
                characterName = charName,
                characterID = charID,
                interactionCount = 1
            });
        }
    }

    public int GetInteractionCharacterByID(string charID)
    {
        var character = _gameState.InteractionCharacters.Find(character => character.characterID == charID);
        
        return character.interactionCount;
    }
}
