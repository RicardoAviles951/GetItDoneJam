using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShockingObject : MonoBehaviour, IElectrifiable
{
    public ParticleSystem electricParticles;
    public UnityEvent triggerEvent;//Created for linking events
    private bool isElectrified = false;//Let's us know if the object is electrified
    [field: SerializeField] public bool useParticleCollisions { get; set; } = false; //Toggle for particle collisions

    //Trigger what happens when the object becomes electrified
    public void Electrify()
    {
        if (isElectrified == false)
        {
            Debug.Log("Electrocuted");
            //Checking to see if there is a particle system referenced or not. 
            if (electricParticles != null)
            {
                electricParticles.Play();
            }
            else
            {
                Debug.LogWarning("Shockable Object: No particles referenced for object");
            }

            //Trigger a custom event
            triggerEvent.Invoke();
            isElectrified = true;

        }
        
    }

    //If using particle collisions to trigger 
    void OnParticleCollision(GameObject other)
    {
        if (useParticleCollisions)
        {
            Electrify();
        }
    }


}
