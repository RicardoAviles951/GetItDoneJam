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
    private void Awake()
    {
        var root     = GetComponent<UIDocument>().rootVisualElement;
        EntireScreen = root.Q<VisualElement>("EntireScreen");
        Container    = root.Q<VisualElement>("Prompts");
        abilityText  = root.Q<Label>("currentAbility");
        healthBar = root.Q(className: "unity-progress-bar__progress");
        
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

    void ShowDisplay()
    {

        EntireScreen.style.display = DisplayStyle.Flex;

    }

    void HideDisplay()
    {
        EntireScreen.style.display = DisplayStyle.None;
    }

    void AbilityChanged(string text)
    {
        abilityText.text = text;
    }

    private void UpdateHealth(float value)
    {
        healthBar.style.width = Length.Percent(value);
    }

}
