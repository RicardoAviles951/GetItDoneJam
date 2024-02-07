using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ExamineUIManager : MonoBehaviour
{
    private VisualElement EntireScreen;
    private Label itemName;
    private Label ItemDescription;
    public int fontSize;
    private void Awake()
    {
        var root        = GetComponent<UIDocument>().rootVisualElement;
        EntireScreen    = root.Q<VisualElement>("EntireScreen");
        itemName        = root.Q<Label>("Label_name");
        ItemDescription = root.Q<Label>("Label_description");
        fontSize = 64;
    }

    private void OnEnable()
    {
        PlayerStateManager.ShowExamineUI += ShowDisplay;
        PlayerStateManager.HideExamineUI += HideDisplay;

        ExamineObject.itemInfo += LoadText;
        
    }

    private void OnDisable()
    {
        PlayerStateManager.ShowExamineUI -= ShowDisplay;
        PlayerStateManager.HideExamineUI -= HideDisplay;

        ExamineObject.itemInfo -= LoadText;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        ItemDescription.style.fontSize = fontSize;
    }


    void ShowDisplay()
    {
        
        EntireScreen.style.display = DisplayStyle.Flex;
        
    }

    void HideDisplay()
    {
        EntireScreen.style.display = DisplayStyle.None;
    }

    void LoadText(ItemInfo iteminfo)
    {
        itemName.text        = iteminfo.ItemName;
        ItemDescription.text = iteminfo.ItemDescription;
    }
    
}
