using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class ElectricityAbility : AbilityBase
{
    IElectrifiable electrifiedObject; 
    public override string Name { get; set; }
    public ElectricityAbility() 
    {
        Name = "Electricity";
    }

    public override void Activate(AbilityManager ability)
    {
        Debug.Log("Current Ability: Electricity");
    }

    public override void UpdateAbility(AbilityManager ability)
    {
        
        if (ability.input.fire)
        {
            //Fire particles if able
            if (ability.particles != null)
            {
                var pm = ability.particles.main;
                pm.startColor = Color.blue;

                ability.particles.Emit(1);
            }
            else
            {
                Debug.LogWarning("The player does not have a particle system referenced for this ability.");
            }
            
             
        }



        //Trigger the electrify method on the shockable object.
        if (ability.input.fire && CheckElemental(ability))
        {
            Debug.Log("electrifying object!");
            electrifiedObject.Electrify();
        }


        //Change ability if it is unlocked
        if (ability.input.change)
        {
            if (ability.abilityData.IsAbilityUnlocked(ability.fireAbility.Name))
            {
                ability.ChangeAbility(ability.fireAbility);
            }

        }
    }

    //Method that checks if the object is indeed our desired type.
    bool CheckElemental(AbilityManager ability)
    {
        bool detected = ability.detector.detected;
        if (detected)
        {
            IElectrifiable electrifiable = ability.detector.hit.collider.gameObject.GetComponent<IElectrifiable>();
            if (electrifiable != null && electrifiable.useParticleCollisions == false)
            {
                electrifiedObject = electrifiable;
                return true;
            }
        }
        return false;
    }
}
