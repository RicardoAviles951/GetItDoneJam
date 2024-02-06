using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Ink.Runtime;
using Ink.UnityIntegration;


public class DialogueManager : MonoBehaviour
{
    public VisualElement root;
    private VisualElement container;
    public Label text;
    public Story currentStory;
    public TextAsset InkJSONAsset;
    public VisualTreeAsset item;
    private Button button;
    private List<Button> buttonList = new List<Button>();
  

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        text = root.Q<Label>("Text_Paragraph");
        container = root.Q<VisualElement>("Choices");
    }


    // Start is called before the first frame update
    void Start()
    {
        // Create a new Story object using the compiled (JSON) Ink story text
        currentStory = new Story(InkJSONAsset.text);
        GenerateDialogue();
        
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

            // For each tag in currentTag, set its values to the new variable 'tag'
            foreach (string tag in currentTags)
            {
                // Concatenate the tag and a colon
                line += tag + ": ";
            }

            // Concatenate the current text chunk
            // (This will either have a tag before it or be by itself.)
            line += currentTextChunk;

            // Concatenate the content of 'line' to the existing text
            text.text += line;

            
            //Displaying choices------------------------------------------------------------------------

            //Creates buttons based on the amount of choices.
            //Fills in the text of the button based on ink choice text.
            foreach (Choice choice in currentStory.currentChoices)
            {
                //Generates a button based on a template created in UI Builder
                TemplateContainer itemButton = item.Instantiate();
                //Add the item to the choices container
                container.Add(itemButton);
               
                //Get's reference to the button
                button = itemButton.Q<Button>("Button");
                
                
                //Create clicked behavior using anonymous function
                button.clicked += () =>
                {
                    Debug.Log("Clicked the button");
                    //Get the index of the choice
                    currentStory.ChooseChoiceIndex(choice.index);
                    //Remove all the buttons
                    container.Clear();
                    
                    //Regenerate the dialogue. 
                    GenerateDialogue();
                    
                };

                //Set's text of button based on the choice index
                button.text = choice.text;
            }

            
            //Separately adjust the width of the buttons so that it adjusts their width in relation to the container they are in.
            foreach (var Child in container.Children())
            {
                //Set the width/height to 100% using the percentage value
                Child.style.width  = Length.Percent(100);
                Child.style.height = Length.Percent(25);
            }

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
}




