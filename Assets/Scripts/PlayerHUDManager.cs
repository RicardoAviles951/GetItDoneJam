using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHUDManager : MonoBehaviour
{
    private VisualElement Container;
    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        Container = root.Q<VisualElement>("Prompts");
    }

    private void OnEnable()
    {
        TriggerPrompt.triggerPrompt += FadePromptIn;
        TriggerPrompt.triggerNoPrompt += FadePromptOut;
    }

    private void OnDisable()
    {
        TriggerPrompt.triggerPrompt -= FadePromptIn;
        TriggerPrompt.triggerNoPrompt -= FadePromptOut;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FadePromptOut()
    {
        Container.AddToClassList("PromptsNotVisible");
    }

    void FadePromptIn()
    {
        Container.RemoveFromClassList("PromptsNotVisible");
    }

}
