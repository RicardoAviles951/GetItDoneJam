using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class ElectricityAbility : AbilityBase
{
    IElectrifiable electrifiedObject;
    bool soundPlayed = false;
    public override string Name { get; set; }

    private bool boomParticlePlayed = false;
    public ElectricityAbility() 
    {
        Name = "Electricity";
    }

    public override void Activate(AbilityManager ability)
    {
        ability.GetAbilityText(Name);

        ParticlePlayer(ability.electricIdle);
        ability.electricSound.Post(ability.gameObject);
        Debug.Log("Current Ability: Electricity");
    }

    public override void UpdateAbility(AbilityManager ability)
    {
        
        if (ability.input.fire)
        {
            if(!soundPlayed)
            {
                //Start playing looping sound once

                soundPlayed = true;
            }
            
            ParticleEmitter(ability.electricShoot, ability.ElectricEmissionCount);
            if (!boomParticlePlayed)
            {
                ParticlePlayer(ability.electricOnce);
                boomParticlePlayed = true;
            }

        }
        else
        {
            //stop looping sound

            soundPlayed = false;
            boomParticlePlayed = false;
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
                StopParticles(ability.electricIdle);
                boomParticlePlayed = false;
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
                Debug.Log("Electrifiable detected");
                electrifiedObject = electrifiable;
                return true;
            }
        }
        return false;
    }

    void ParticlePlayer(List<ParticleSystem> particles)
    {
        foreach(ParticleSystem particle in particles)
        {
            if(particle != null)
            {
                particle.Play();
            }
            else
            {
                Debug.LogWarning("The player does not have a particle system referenced for this ability.");
            }

        }
    }
    void ParticleEmitter(List<ParticleSystem> particles, int number)
    {
        foreach(ParticleSystem particle in particles)
        {
            if(particle != null)
            {
                particle.Emit(number);
            }
            else
            {
                Debug.LogWarning("The player does not have a particle system referenced for this ability.");
            }

        }
    }
    void StopParticles(List<ParticleSystem> particles)
    {
        foreach (ParticleSystem particle in particles)
        {
            if(particle != null)
            {
                particle.Stop();
            }
            else
            {
                Debug.LogWarning("The player does not have a particle system referenced for this ability.");
            }

        }
    }

}
