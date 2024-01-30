using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "Progression/AbilityData")]
public class SO_AbilityData : ScriptableObject
{
    [SerializeField]
    private List<string> unlockedAbilities = new List<string>();

    public bool IsAbilityUnlocked(string abilityName)
    {
        return unlockedAbilities.Contains(abilityName);
    }

    public void UnlockAbility(string abilityName)
    {
        if (!unlockedAbilities.Contains(abilityName))
        {
            unlockedAbilities.Add(abilityName);
        }
    }

    public void LockAbility(string abilityName)
    {
        if (unlockedAbilities.Contains(abilityName))
        {
            unlockedAbilities.Remove(abilityName);
        }
    }
}
