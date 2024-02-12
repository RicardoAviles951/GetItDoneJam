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
    //Sends message when player is out of health
    public static event Action OnDeath;
    //Custom class to hold information about health UI
    private HealthBarInfo healthBarInfo;
    
    //Particles to play when getting health related effects
    public ParticleSystem buffParticles;
    public ParticleSystem debuffParticles;

    public float regen;
    public float totalHealth;
    public float currentHealth;
    public float regenDelay = 3;
    public AK.Wwise.Event damageSound;
    public AK.Wwise.Event regenSound;
    private bool playingSound = false;

    private float timer = 0;
    private bool isHurt;

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
        if (timer < regenDelay)
        {
            timer += Time.deltaTime;
        }
        else
        {
            //Allow health regen.
            RegenHealth();
        }
        
    }

    public void TakeDamage(float damage)
    {
        if(currentHealth > damage)
        {
            //Subtract damage from current health
            currentHealth -= damage;
            //Reset regen timer. 
            timer = 0;
            isHurt = true;

            //For UI updates

            //Calculate the ration between health and max health.
            GetHealthRatio();
            //Send message to UI 
            OnHealthChange?.Invoke(healthBarInfo);
        }
        else if(currentHealth <= damage) 
        {
            Debug.Log("Player health gone");
            //Stops regen
            isHurt = false;
            //Sets health exactly to 0
            currentHealth = 0;

            //For UI
            GetHealthRatio();
            OnHealthChange?.Invoke(healthBarInfo);

            //Notify anything waiting for player death.
            OnDeath?.Invoke();
            
        }

        regenSound.Stop(gameObject);
        damageSound.Post(gameObject);
    }

    void RegenHealth()
    {
        if (isHurt)
        {
            currentHealth += regen * Time.deltaTime;

            //For UI
            GetHealthRatio();
            //Change the health UI
            OnHealthChange?.Invoke(healthBarInfo);
            if (!playingSound)
            {
                regenSound.Post(gameObject);
                buffParticles.Play();
                playingSound = true;
            }
        }

        if (currentHealth > totalHealth)
        {
            //Turn off regen
            isHurt = false;
            //Set health to max
            currentHealth = totalHealth;

            //For UI
            GetHealthRatio();
            //Change the health
            OnHealthChange?.Invoke(healthBarInfo);
            playingSound = false;
            regenSound.Stop(gameObject);
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

}
