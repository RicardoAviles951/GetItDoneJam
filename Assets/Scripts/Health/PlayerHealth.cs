using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    private void Awake()
    {
        instance = this;
    }

    public float regen;
    public float totalHealth;
    public float currentHealth;
    public Slider health;
    private bool isHurt;

    private void Update()
    {
        if (isHurt)
        {
            PlayerHealth.instance.currentHealth += regen * Time.deltaTime;
            health.value = currentHealth;
        }
        if (PlayerHealth.instance.currentHealth > PlayerHealth.instance.totalHealth)
        {
            isHurt = false;
            PlayerHealth.instance.currentHealth = PlayerHealth.instance.totalHealth;
            health.value = currentHealth;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        health.maxValue = totalHealth;
        currentHealth = totalHealth;
        health.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        health.value = currentHealth;
        isHurt=true;
    }

}
