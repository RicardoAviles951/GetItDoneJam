using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Ink.Runtime;
using System;

public enum Outcome
{
    healthDown,
    speedDown,
    speedUp,
    healthUp,
    nothing
}
public class DialogueManager : MonoBehaviour
{
    public static event Action DialogueFinished;
    public static event Action<Outcome> PlayerStatusApplied;

    private VisualElement choicesContainer;
    public Label text;
    public Story currentStory;

    private VisualElement EntireScreen;
    public VisualTreeAsset item;
    private Button button;
    private List<Button> buttonList = new List<Button>();
    

    public Outcome currentOutcome; 
    //Sounds
    public AK.Wwise.Event clickSound;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        EntireScreen = root.Q<VisualElement>("EntireScreen");
        text = root.Q<Label>("Text_Paragraph");
        choicesContainer = root.Q<VisualElement>("Choices");

        PlayerStateManager.ShowDialogue += ShowDisplay;
        PlayerStateManager.HideDialogue += HideDisplay;

        ConsoleController.SendInkFile += LoadText;


    }

    private void OnDisable()
    {
        PlayerStateManager.ShowDialogue -= ShowDisplay;
        PlayerStateManager.HideDialogue -= HideDisplay;

        ConsoleController.SendInkFile -= LoadText;



    }

    // Start is called before the first frame update
    void Start()
    {
        //Start with the dialogue screen not showing
        EntireScreen.style.display = DisplayStyle.None;
        currentOutcome = Outcome.nothing;
    }

        

    void GenerateDialogue()
    {
        //Set the paragraph text to nothing. 
        text.text = "";
        
        // Each loop, check if there is more story to load
        while (currentStory.canContinue)
        {
            // Load the next story chunk and return the current text
            string currentTextChunk = currentStory.Continue();

            // Get any tags loaded in the current story chunk
            List<string> currentTags = currentStory.currentTags;

            // Create a blank line of dialogue
            string line = "";

            //For each tag in currentTag, set its values to the new variable 'tag'
            foreach (string tag in currentTags)
            {
                switch(tag)
                {
                    case "outcome1":
                        Debug.Log("Effect: General health decrease.");
                        currentOutcome = Outcome.healthDown;
                        break;
                    case "outcome2": 
                        Debug.Log("Effect: General movement decrease.");
                        currentOutcome = Outcome.speedDown;
                        break;
                    case "outcome3": 
                        Debug.Log("Effect: General movement increase.");
                        currentOutcome = Outcome.speedUp;
                        break;
                    case "outcome4": 
                        Debug.Log("Effect: General health increase.");
                        currentOutcome = Outcome.healthUp;
                        break;
                    default: 
                        Debug.Log("No important tags detected");
                        
                        break;
                }
            }

            // Concatenate the current text chunk
            // (This will either have a tag before it or be by itself.)
            line += currentTextChunk;

            // Concatenate the content of 'line' to the existing text
            text.text += "\n"+ line;

            
            //Displaying choices------------------------------------------------------------------------

            //Creates buttons based on the amount of choices.
            //Fills in the text of the button based on ink choice text.
            foreach (Choice choice in currentStory.currentChoices)
            {
                //Generates a button based on a template created in UI Builder
                TemplateContainer itemButton = item.Instantiate();
                //Add the item to the choices container
                choicesContainer.Add(itemButton);
               
                //Get's reference to the button
                button = itemButton.Q<Button>("Button");
                
                
                //Create clicked behavior using anonymous function
                button.clicked += () =>
                {
                    //Play sound
                    clickSound.Post(gameObject);
                    Debug.Log("Clicked the button");
                    //Get the index of the choice
                    currentStory.ChooseChoiceIndex(choice.index);
                    //Remove all the buttons
                    choicesContainer.Clear();
                    
                    //Regenerate the dialogue. 
                    GenerateDialogue();
                    
                };

                //Set's text of button based on the choice index
                button.text = choice.text;
            }

            
            //Separately adjust the width of the buttons so that it adjusts their width in relation to the container they are in.
            foreach (var Child in choicesContainer.Children())
            {
                //Set the width/height to 100% using the percentage value
                Child.style.width  = Length.Percent(100);
                Child.style.height = Length.Percent(80);
            }

        }

        if(currentStory.currentChoices.Count == 0)
        {
            HideDisplay();
            if (currentOutcome != Outcome.nothing)
            {
                Debug.Log("Relaying status effect...");
                PlayerStatusApplied?.Invoke(currentOutcome);
            }
            DialogueFinished?.Invoke();
            currentOutcome = Outcome.nothing;
            Debug.Log("Story over");

            
        }
        
        
    }


    void FadeInText()
    {
        if (text.ClassListContains("InvisibleText"))
        {
            text.RemoveFromClassList("InvisibleText");
        }
        
    }

    void FadeOutText()
    {
        if (!text.ClassListContains("InvisibleText"))
        {
            text.AddToClassList("InvisibleText");
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

    void LoadText(TextAsset text)
    {
        ShowDisplay();
        currentStory = new Story(text.text);
        GenerateDialogue();
    }
}




