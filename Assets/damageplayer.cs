using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamagePlayer : MonoBehaviour
{
    public float damageCooldown;
    public float damage;
    public float maxDamage;
    public bool  canDamage = true;

    private PlayerHealth player;
    private float timer = 0;

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            Debug.Log("Player detected");
            player.TakeDamage(damage);
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(canDamage)
        {
            if (timer < damageCooldown)
            {
                timer += Time.deltaTime;
            }
            else
            {
                if(player != null)
                {
                    float randDamage = Random.Range(damage, maxDamage);
                    player.TakeDamage(randDamage); // Apply damage only if cooldown period has elapsed
                }
                
                timer = 0; // Reset the timer
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 pos = transform.position;
        Gizmos.DrawCube(pos, Vector3.one);
    }


}
