using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyAbility : AbilityBase
{
    public override string Name { get; set; } = "";

    public override void Activate(AbilityManager ability)
    {
        ability.GetAbilityText(Name);
        //Debug.Log("No ability");
    }

    public override void UpdateAbility(AbilityManager ability)
    {

        //Change ability if it is unlocked
        if (ability.input.change)
        {
            if(ability.abilityData.IsAbilityUnlocked(ability.electricityAbility.Name))
            {
                ability.ChangeAbility(ability.electricityAbility);
            }
            
        }
    }
}
