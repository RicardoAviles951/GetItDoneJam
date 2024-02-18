using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System;
using UnityEngine.Events;

public class ConsoleController : MonoBehaviour, IDialogue
{
    //-----------------Events---------------------------------------//
    public UnityEvent EndingDialogueEvent;
    public static event Action<TextAsset> SendInkFile;
    //--------------------------------------------------------------//

    //-----------------Fields---------------------------------------//
    public TextAsset InkDialogue;
    public AK.Wwise.Event errorSound;
    private int counter = 0;
    private bool isBeingUsed = false;
    //--------------------------------------------------------------//

    //--------------Interface Implementations-----------------------//
    public string name { get; set; }
    [field: SerializeField] public bool isRepeatable { get; set; } = true;

    public void LoadDialogue()
    {
        Debug.Log("Loading Dialogue...");
        //Relays the inkFile information to the DialogueManager
        SendInkFile.Invoke(InkDialogue);

        isBeingUsed = true;
        //For keeping track of non-repeatable consoles.
        counter++;
    }
    public bool HasDialogue()
    {
        if (InkDialogue != null)
        {
            return true;
        }
        else
        {
            Debug.LogWarning("NO INK FILE ATTACHED.");
            return false;
        }
    }

    public bool CheckForRepeatable()
    {
        if (!isRepeatable && (counter > 0))
        {
            Debug.Log("Cannot use console again.");
            errorSound.Post(gameObject);
            return false;
        }
        else if (!isRepeatable && (counter == 0))
        {
            Debug.Log("First time on a non-repeatable");
            return true;
        }
        else
        {
            Debug.Log("Repeatable console");
            return true;
        }
    }
    //-------------------------------------------------------------//

    void OnEnable() => DialogueManager.DialogueFinished += EndingEvent;
    void OnDisable() => DialogueManager.DialogueFinished -= EndingEvent;
    void Awake() => name = gameObject.name;
    void EndingEvent()
    {
        if(isBeingUsed) 
        {
            EndingDialogueEvent.Invoke();
            isBeingUsed = false;
        }
        

    }
}
