using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //Added by Ricardo
    //Create events that other classes can subscribe to
    public static event Action<HealthBarInfo> OnHealthChange;
    //Custom class to hold information about health UI
    private HealthBarInfo healthBarInfo;
    //Sends message when player is out of health
    public static event Action OnDeath;

    public ParticleSystem buffParticles;
    public ParticleSystem debuffParticles;

    public float regen;
    public float totalHealth;
    public float currentHealth;
    private bool isHurt;
    public float regenDelay = 2;

    //Added by Ricardo
    private float healthRatio;
    void OnEnable()
    {
        DialogueManager.PlayerStatusApplied += HealthStatus;
    }

    private void OnDisable()
    {
        DialogueManager.PlayerStatusApplied -= HealthStatus;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
        healthBarInfo = new HealthBarInfo(totalHealth, currentHealth);
        //Set health bar at start
        OnHealthChange?.Invoke(healthBarInfo);
    }
    private void Update()
    {
        if (isHurt)
        {
            currentHealth += regen * Time.deltaTime;
            GetHealthRatio();
            
            //Change the health
            OnHealthChange?.Invoke(healthBarInfo);
        }

        if (currentHealth > totalHealth)
        {
            isHurt = false;
            currentHealth = totalHealth;
            GetHealthRatio();
            //Change the health
            OnHealthChange?.Invoke(healthBarInfo);
        }
    }

    public void TakeDamage(float damage)
    {
        if(currentHealth > damage)
        {
            currentHealth -= damage;
            StartCoroutine(RegenDelay());
            GetHealthRatio();
            OnHealthChange?.Invoke(healthBarInfo);
            
        }
        else if(currentHealth <= damage) 
        {
            Debug.Log("Player health gone");
            //Stops regen
            isHurt = false;
            //Sets health exactly to 0
            currentHealth = 0;
            GetHealthRatio();
            OnHealthChange?.Invoke(healthBarInfo);
            //Notify anything waiting for player death.
            OnDeath?.Invoke();
        }
    }

    void GetHealthRatio()
    {
        healthBarInfo.currentHealth = currentHealth;
        healthBarInfo.maxHealth     = totalHealth;
        healthBarInfo.healthRatio   = healthRatio;

    }

    void HealthStatus(Outcome status)
    {
        switch (status)
        {
            case Outcome.healthDown:
                currentHealth -= 20f;
                totalHealth = 80;
                GetHealthRatio();
                OnHealthChange?.Invoke(healthBarInfo);
                debuffParticles.Play();
                Debug.Log("Health dropped by 20");
                break;

            case Outcome.healthUp:
                totalHealth = 120;
                currentHealth += 20f;
                GetHealthRatio();
                OnHealthChange?.Invoke(healthBarInfo);
                buffParticles.Play();
                Debug.Log("Health increased by 20");
                break;

            default: Debug.Log("No health related status affects applied"); break;
        }
    }

    IEnumerator RegenDelay()
    {
        yield return new WaitForSeconds(regenDelay);
        isHurt = true;
    }

}
