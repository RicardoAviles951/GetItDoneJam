using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class SubtitleManager : MonoBehaviour
{
    public VisualElement prompts;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        prompts = root.Q<VisualElement>("Prompts");
    }

    public void FadePromptOut()
    {
        if (!prompts.ClassListContains("PromptsNotVisible"))
        {
            prompts.AddToClassList("PromptsNotVisible");
        }
        
    }

    public void FadePromptIn()
    {
       
        if (prompts.ClassListContains("PromptsNotVisible"))
        {
            prompts.RemoveFromClassList("PromptsNotVisible");
        }

    }
}
