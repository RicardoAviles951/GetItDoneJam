using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

public class PlayerHUDManager : MonoBehaviour
{
    private VisualElement EntireScreen;
    private VisualElement Container;
    private Label abilityText;
    private VisualElement healthBar;
    private VisualElement maxHealthBar;
    private VisualElement baseImage;

    

    private void OnEnable()
    {
        var root     = GetComponent<UIDocument>().rootVisualElement;
        EntireScreen = root.Q<VisualElement>("EntireScreen");
        Container    = root.Q<VisualElement>("Prompts");
        abilityText  = root.Q<Label>("currentAbility");

        baseImage     = root.Q<VisualElement>("base");

        healthBar    = root.Q<VisualElement>("healthProgress");
        maxHealthBar = root.Q<VisualElement>("healthBackground");



        TriggerPrompt.triggerPrompt      += FadePromptIn;
        TriggerPrompt.triggerNoPrompt    += FadePromptOut;

        CameraDetector.triggerPrompt     += FadePromptIn;
        CameraDetector.triggerNoPrompt   += FadePromptOut;

        PlayerStateManager.ShowPlayerHUD += ShowDisplay;
        PlayerStateManager.HidePlayerHUD += HideDisplay;

        AbilityManager.SendAbilityUI     += AbilityChanged;

        PlayerHealth.OnHealthChange      += UpdateHealth;
        PlayerHealth.OnHealthEffect      += UpdateMinMaxHealth;

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
        PlayerHealth.OnHealthEffect      -= UpdateMinMaxHealth;
    }

    private void Start()
    {
        //healthBounds.style.width = Length.Percent(40);
        maxHealthBar.style.width = Length.Percent(40);
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
    void AbilityChanged(AbilityInfo ability)
    {
        //abilityText.text = ability.Name;

        // Assuming you have a Texture2D that you want to set as the background image
        Texture2D newBackgroundTexture = Resources.Load<Texture2D>("Abilities/"+ability.file);

        // Check if the baseImage has a style and if the newBackgroundTexture is not null
        if (baseImage.style != null)
        {
            if (newBackgroundTexture != null)
            {
                // Set the background image
                baseImage.style.backgroundImage = newBackgroundTexture;
            }
            else
            {
                Debug.LogWarning("Couldn't load texture");
            }

        }
        else
        {
            Debug.LogWarning("Visual element null");
        }
    }


    private void UpdateHealth(HealthBarInfo healthbarInfo)
    {
        float duration = 0.25f;
        float hp = healthBar.style.width.value.value;

        // Tween the health bar width directly
        DOTween.To(() => healthBar.style.width.value.value, x => healthBar.style.width = Length.Percent(x), healthbarInfo.healthRatio*100, duration).SetEase(Ease.OutCubic);
    }

    private void UpdateMinMaxHealth(float delta)
    {
        //cache width of UI health bar container
        float maxHealth = maxHealthBar.style.width.value.value;
        //Calculate the amount of health to be taken away
        float per = maxHealth * delta / 100; //Convert to a value we can use
        //Round to a clean number
        float newHealth = Mathf.Round((maxHealth + delta));
        //duration of tween
        float barDuration = .25f;

        //Tween bar to desired ratio
        DOTween.To(() => maxHealth, y => maxHealthBar.style.width = Length.Percent(y), newHealth, barDuration);

    }


}
