using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BurningObject : MonoBehaviour, IBurnable
{
    public ParticleSystem burnParticle;
    private bool isBurning = false;
    [field: SerializeField] public bool useParticleCollisions { get; set; } = false;

    //Trigger what happens when the object becomes burned
    public void Burn()
    {
        if (isBurning == false)
        {
            Debug.Log("Burning!");
            if (burnParticle != null)
            {
                burnParticle.Play();
            }
            else
            {
                Debug.LogWarning("Burning Object: No particle system referenced on object");
            }
            isBurning = true;
        }
        else
        {
            Debug.Log("Already burning!");
        }
        
        
        
    }

    //If using particle collisions to trigger 
    void OnParticleCollision(GameObject other)
    {
        if(useParticleCollisions)
        {
            Burn();
        }        
    }

}
