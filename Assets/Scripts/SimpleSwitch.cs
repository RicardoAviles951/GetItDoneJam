using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleSwitch : MonoBehaviour, IElectrifiable
{
    public UnityEvent LinkedEvent;

    [Tooltip("Particles that play when the switch is electrified")]
    public ParticleSystem electricParticles;
    private bool isElectrified = false;
    [Tooltip("How long until the switch can be fired again.")]
    public float cooldown = 0.5f;
    [field: SerializeField] public bool useParticleCollisions { get; set; } = false; //Toggle for particle collisions

    public GameObject redBoxObj, greenBoxObj;

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
                Debug.LogWarning("Switch Object: No particles referenced for object");
            }

            if (redBoxObj && greenBoxObj)
            {
                redBoxObj.SetActive(false);
                greenBoxObj.SetActive(true);
            }

            //Trigger a custom event
            LinkedEvent.Invoke();
            isElectrified = true;

            //Timer for setting electrified back to false
            StartCoroutine(CoolDown());

        }

    }

    //If using particle collisions to trigger 
    void OnParticleCollision(GameObject other)
    {
       //The particle systems are tagged 'Fire' or 'Electric'
        if (useParticleCollisions && other.tag == "Electric")
        {
            Electrify();
        }
        Debug.Log("Collided");
    }

    IEnumerator CoolDown()
    {
        Debug.Log("Cooling down...");
        yield return new WaitForSeconds(cooldown);
        isElectrified = false;
    }
}
