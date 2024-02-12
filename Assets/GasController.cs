using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GasController : MonoBehaviour
{
    public UnityEvent releaseGas;
    public bool hasReleasedGas = false;
    public List<ParticleSystem> particleSystems = new List<ParticleSystem>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!hasReleasedGas)
            {
                foreach(ParticleSystem p in particleSystems)
                {
                    if(p !=null)
                    {
                        p.Play();
                    }
                    else
                    {
                        Debug.LogWarning("Missing particles for gas trigger");
                    }
                    
                }
                hasReleasedGas = true;
            }
        }
    }

}
