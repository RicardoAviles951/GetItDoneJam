using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class damageplayer : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            Debug.Log("Player detected");
            player.TakeDamage(20f);
        }
        //Debug.Log("Start value" + startValue);
        
    }

    
}
