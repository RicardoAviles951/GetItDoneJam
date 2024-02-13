using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireAbility : AbilityBase
{
    IBurnable burnedObject;
    bool boomParticlePlayed = false;

    public override string Name {get; set;}

    public FireAbility()
    {
        Name = "Fire";
    }
    public override void Activate(AbilityManager ability)
    {
        ability.GetAbilityText(Name);

        ParticlePlayer(ability.fireIdle);


        Debug.Log("Current Ability: Fire");
    }

    public override void UpdateAbility(AbilityManager ability)
    {
        //Fire ability
        if (ability.input.fire)
        {
            Debug.Log("Firing fire");
            //Fire particles if able
            ParticleEmitter(ability.fireShoot, ability.FireEmissionCount);
            if (!boomParticlePlayed)
            {
                ParticlePlayer(ability.fireOnce);
                boomParticlePlayed = true;
            }

        }
        else
        {
            boomParticlePlayed=true;
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
                StopParticles(ability.fireIdle);
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


    void ParticlePlayer(List<ParticleSystem> particles)
    {
        foreach (ParticleSystem particle in particles)
        {
            if (particle != null)
            {
                particle.Play();
            }

        }
    }
    void ParticleEmitter(List<ParticleSystem> particles, int number)
    {
        foreach (ParticleSystem particle in particles)
        {
            if (particle != null)
            {
                particle.Emit(number);
            }

        }
    }
    void StopParticles(List<ParticleSystem> particles)
    {
        foreach (ParticleSystem particle in particles)
        {
            if (particle != null)
            {
                particle.Stop();
            }

        }
    }
}
