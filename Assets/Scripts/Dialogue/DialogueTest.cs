using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Ink.Runtime;


public class DialogueTest : MonoBehaviour
{
    public VisualElement root;
    public Label text;
    public Story currentStory;
    public TextAsset inkFile1;
    public TextAsset InkJSONAsset;
    public VisualTreeAsset item;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        text = root.Q<Label>("Text_Paragraph");
    }
    // Start is called before the first frame update
    void Start()
    {
        GenerateDialogue(InkJSONAsset);
    }

    

    void GenerateDialogue(TextAsset inkFile)
    {
        // Create a new Story object using the compiled (JSON) Ink story text
        Story exampleStory = new Story(inkFile.text);

        // Each loop, check if there is more story to load
        while (exampleStory.canContinue)
        {
            // Load the next story chunk and return the current text
            string currentTextChunk = exampleStory.Continue();

            // Get any tags loaded in the current story chunk
            List<string> currentTags = exampleStory.currentTags;

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

            //Displaying choices-----------------------------------------------
            //Get reference to the choices container
            VisualElement container = root.Q<VisualElement>("Choices");

            //Creates buttons based on the amount of choices.
            //Fills in the text of the button based on ink choice text.
            foreach (Choice choice in exampleStory.currentChoices)
            {
                TemplateContainer itemButton = item.Instantiate();
                container.Add(itemButton);
               
                Button button = itemButton.Q<Button>("Button");             
                button.text = choice.text;
            }
            

            //Separately adjust the width of the buttons so that it adjusts their width in relation to the container they are in.
            foreach(var Child in container.Children())
            {
                //Set the width to 100% using the percentage value
                Child.style.width = Length.Percent(100);
            }

        }
    }
    

    
}
