using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarInfo
{
    public float maxHealth;
    public float currentHealth;
    public float healthRatio;

    public HealthBarInfo(float maxHP, float HP)
    {
        maxHealth     = maxHP;
        currentHealth = HP; 

        healthRatio   = HP / maxHP;

        if(healthRatio < 0)
        {
            healthRatio = 0;
        }
    }
}
