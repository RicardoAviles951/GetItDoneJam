using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ExamineUIManager : MonoBehaviour
{
    private VisualElement EntireScreen;
    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        EntireScreen = root.Q<VisualElement>("EntireScreen");
    }

    private void OnEnable()
    {
        PlayerStateManager.ShowExamineUI += ShowDisplay;
        PlayerStateManager.HideExamineUI += HideDisplay;
        
    }

    private void OnDisable()
    {
        PlayerStateManager.ShowExamineUI -= ShowDisplay;
        PlayerStateManager.HideExamineUI -= HideDisplay;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void ShowDisplay()
    {
        
        EntireScreen.style.display = DisplayStyle.Flex;
        
    }

    void HideDisplay()
    {
        EntireScreen.style.display = DisplayStyle.None;
    }

    
}
