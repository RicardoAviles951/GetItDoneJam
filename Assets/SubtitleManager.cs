using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class SubtitleManager : MonoBehaviour
{
    public VisualElement fadeScreen;
    public VisualElement prompts;

    private void OnEnable()
    {
        PlayerStateManager.ShowOpeningUI += FadeIn;
    }

    private void OnDisable()
    {
        PlayerStateManager.ShowOpeningUI -= FadeIn;
    }
    // Start is called before the first frame update
    void Start()
    {
        var root    = GetComponent<UIDocument>().rootVisualElement;
        fadeScreen  = root.Q<VisualElement>("Fade");
        prompts     = root.Q<VisualElement>("Prompts");
        StartCoroutine(Delay());
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

    public void FadeOut()
    {
        if (!fadeScreen.ClassListContains("screen_black"))
        {
            fadeScreen.AddToClassList("screen_black");
        }
    }

    public void FadeIn()
    {
        Debug.Log("Fading in");
        if (fadeScreen.ClassListContains("screen_black"))
        {
            fadeScreen.RemoveFromClassList("screen_black");
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        fadeScreen.RemoveFromClassList("screen_black");
    }



}
