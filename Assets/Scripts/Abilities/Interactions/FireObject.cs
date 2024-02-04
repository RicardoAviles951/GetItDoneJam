using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FireObject : MonoBehaviour
{
    private Rigidbody rb;
    public ParticleSystem particles;
    [SerializeField] private float power = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootDirection()
    {
        rb.AddForce(Vector3.forward * power, ForceMode.Impulse);
    }
    public void PlayParticle()
    {
        if(particles != null)
        {
            particles.Play();
        }
        else
        {
            Debug.LogWarning("No particle system referenced.");
        }
        
    }
}
