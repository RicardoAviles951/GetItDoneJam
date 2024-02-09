using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //Added by Ricardo
    //Create events that other classes can subscribe to
    public static event Action<float> OnHealthChange;
    public static event Action OnDeath;

    public float regen;
    public float totalHealth;
    public float currentHealth;
    private bool isHurt;

    //Added by Ricardo
    private float healthRatio;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
        //Set health bar at start
        OnHealthChange?.Invoke(GetHealthRatio());
    }
    private void Update()
    {
        if (isHurt)
        {
            currentHealth += regen * Time.deltaTime;
            
            //Change the health
            OnHealthChange?.Invoke(GetHealthRatio());
        }

        if (currentHealth > totalHealth)
        {
            isHurt = false;
            currentHealth = totalHealth;

            //Change the health
            OnHealthChange?.Invoke(GetHealthRatio());
        }
    }

    public void TakeDamage(float damage)
    {
        if(currentHealth > 0)
        {
            currentHealth -= damage;
            OnHealthChange?.Invoke(GetHealthRatio());
            isHurt = true;
        }
        else if(currentHealth <= 0) 
        {
            //Stops regen
            isHurt = false;
            //Sets health exactly to 0
            currentHealth = 0;
            //Notify anything waiting for player death.
            OnDeath?.Invoke();
        }
    }

    float GetHealthRatio()
    {
        //Get ratio of health to max health (returns decimal)
        float ratio = (currentHealth / totalHealth);
        healthRatio = ratio;

        //Check that player health didn't go below 0
        if(healthRatio < 0)
        {
            return 0f;
        }

        //prepare the value to be turned into a percent for UI.
        float percentHealth = healthRatio * 100;
        return percentHealth;
    }

}
