using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHUDManager : MonoBehaviour
{
    private VisualElement EntireScreen;
    private VisualElement Container;
    private Label abilityText;
    private VisualElement healthBar;
    private VisualElement maxHealthBar;
    private void Awake()
    {
        var root     = GetComponent<UIDocument>().rootVisualElement;
        EntireScreen = root.Q<VisualElement>("EntireScreen");
        Container    = root.Q<VisualElement>("Prompts");
        abilityText  = root.Q<Label>("currentAbility");
        healthBar    = root.Q(className: "unity-progress-bar__progress");
        maxHealthBar = root.Q(className: "unity-progress-bar__container");
        
    }

    private void OnEnable()
    {
        TriggerPrompt.triggerPrompt      += FadePromptIn;
        TriggerPrompt.triggerNoPrompt    += FadePromptOut;

        CameraDetector.triggerPrompt     += FadePromptIn;
        CameraDetector.triggerNoPrompt   += FadePromptOut;

        PlayerStateManager.ShowPlayerHUD += ShowDisplay;
        PlayerStateManager.HidePlayerHUD += HideDisplay;

        AbilityManager.SendAbilityUI     += AbilityChanged;

        PlayerHealth.OnHealthChange      += UpdateHealth;

    }

    private void OnDisable()
    {
        TriggerPrompt.triggerPrompt      -= FadePromptIn;
        TriggerPrompt.triggerNoPrompt    -= FadePromptOut;

        CameraDetector.triggerPrompt     -= FadePromptIn;
        CameraDetector.triggerNoPrompt   -= FadePromptOut;

        PlayerStateManager.ShowPlayerHUD -= ShowDisplay;
        PlayerStateManager.HidePlayerHUD -= HideDisplay;

        AbilityManager.SendAbilityUI     -= AbilityChanged;

        PlayerHealth.OnHealthChange      -= UpdateHealth;
    }
    

    void FadePromptOut()
    {
        if (!Container.ClassListContains("PromptsNotVisible"))
        {
            Container.AddToClassList("PromptsNotVisible");
        }
    }

    void FadePromptIn()
    {
        if (Container.ClassListContains("PromptsNotVisible"))
        {
            Container.RemoveFromClassList("PromptsNotVisible");
        }
        
    }

    void ShowDisplay() => EntireScreen.style.display = DisplayStyle.Flex;
    void HideDisplay() => EntireScreen.style.display = DisplayStyle.None;
    void AbilityChanged(string text) => abilityText.text = text;


    private void UpdateHealth(HealthBarInfo healtbarInfo)
    {   
        float duration = 0.25f;
        float maxHealth = maxHealthBar.style.width.value.value;


        if (maxHealth < healtbarInfo.maxHealth)
        {
            //if the max is increasing have the container move faster that the health bar. 
            float barDuration = .12f;
            DOTween.To(() => maxHealthBar.style.width.value.value, y => maxHealthBar.style.width = Length.Percent(y), healtbarInfo.maxHealth, barDuration);
        }
        else if(maxHealth > healtbarInfo.maxHealth)
        {
            //If the max is decreasing then have the progress bar move faster than the container
            float barDuration = .5f;
            DOTween.To(() => maxHealthBar.style.width.value.value, y => maxHealthBar.style.width = Length.Percent(y), healtbarInfo.maxHealth, barDuration);
        }
        
        // Tween the health bar width directly
        DOTween.To(() => healthBar.style.width.value.value, x => healthBar.style.width = Length.Percent(x), healtbarInfo.currentHealth, duration).SetEase(Ease.OutCubic);

        // Log incoming health value
        //Debug.Log("Incoming Health: " + healtbarInfo.currentHealth);
    }

}
