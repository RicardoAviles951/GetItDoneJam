using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class ConsoleController : MonoBehaviour, IDialogue
{
    public TextAsset InkDialogue;
    public Story currentDialogue;

    public void HideDialogue()
    {
        
    }

    public void ShowDialogue()
    {
        
    }

    void Start()
    {
        if(InkDialogue != null)
        {
            currentDialogue = new Story(InkDialogue.text);
        }
    }

    
}
