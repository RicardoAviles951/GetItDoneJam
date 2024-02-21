using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ItemBurner : MonoBehaviour, IBurnable
{
    public bool isBurning { get; set; } = false; 
    public bool burnReady = false;
    private ItemPlacer itemPlacer;
    public ParticleSystem burnParticles;
    public AK.Wwise.Event notReadyToBurn;

    public bool useParticleCollisions { get; set; } = false;

    void Awake()
    {
        itemPlacer = GetComponent<ItemPlacer>();
    }
    

    void OnEnable() =>  itemPlacer.AllItemsPlaced += ReadyToBurn;
    void OnDisable() => itemPlacer.AllItemsPlaced -= ReadyToBurn;
    

    void ReadyToBurn()
    {
        burnReady = true;
        Debug.Log("Ready to burn items");
    }
    public void Burn()
    {
        if (burnReady)
        {
            if (!isBurning)
            {
                if (burnParticles != null)
                {
                    burnParticles.Play();
                }
                else
                {
                    Debug.LogWarning("No particle system referenced. Attach in inspector.");
                }
            }
            else
            {
                Debug.Log("Already burning!");
            }
        }
        else
        {
            notReadyToBurn.Post(gameObject);
        }
        
        
    }
}
