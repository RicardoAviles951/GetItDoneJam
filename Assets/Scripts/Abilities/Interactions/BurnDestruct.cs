using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDestruct : MonoBehaviour, IBurnable
{
    public ParticleSystem burnParticle;
    public bool isBurning = false;
    private float timer;
    public float burnTime = 3.0f;
    public bool canDestruct = true;
    [field: SerializeField] public bool useParticleCollisions { get; set; } = false;


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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If object is burning, then increment the destruction timer
        if (isBurning && canDestruct)
        {
            
            if(timer > burnTime)
            {
                isBurning = false;
                burnParticle.Stop();
                Destruct();
            }
            timer += Time.deltaTime;
        }
    }

    //If using particle collisions to trigger 
    void OnParticleCollision(GameObject other)
    {
        if (useParticleCollisions)
        {
            Burn();
        }
    }

    void Destruct()
    {
        Destroy(gameObject);
    }
}
