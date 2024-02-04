using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireAbility : AbilityBase
{
    IBurnable burnedObject;

    public override string Name {get; set;}

    public FireAbility()
    {
        Name = "Fire";
    }
    public override void Activate(AbilityManager ability)
    {
        ability.GetAbilityText(Name);

        Debug.Log("Current Ability: Fire");
    }

    public override void UpdateAbility(AbilityManager ability)
    {
        //Fire ability
        if (ability.input.fire)
        {
            Debug.Log("Firing fire");
            //Fire particles if able
            if(ability.particles != null)
            {
                //Placeholder particles being used so this block of code may become obsolete
                var pm = ability.particles.main;
                pm.startColor = Color.red;

                ability.particles.Emit(1);
            }
            else
            {
                Debug.LogWarning("The player does not have a particle system referenced.");
            }
            
        }

        //Trigger the burn method on the burnable object.
        if(ability.input.fire && CheckElemental(ability))
        {
            burnedObject.Burn();
        }


        //Change ability if it is unlocked
        if (ability.input.change)
        {
            if (ability.abilityData.IsAbilityUnlocked(ability.electricityAbility.Name))
            {
                ability.ChangeAbility(ability.electricityAbility);
            }

        }
    }

    //Method that checks if the object is indeed our desired type.
    bool CheckElemental(AbilityManager ability)
    {
        bool detected = ability.detector.detected;
        if (detected)
        {
            IBurnable burnable = ability.detector.hit.collider.gameObject.GetComponent<IBurnable>();
            if (burnable != null && burnable.useParticleCollisions == false)
            {
                burnedObject = burnable;
                return true;
            }
        }
        return false;
    }

}
