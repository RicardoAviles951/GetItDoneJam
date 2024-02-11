using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System;

public class ConsoleController : MonoBehaviour, IDialogue
{
    public string name { get; set; }
    
    public static event Action<TextAsset> SendInkFile;
    public TextAsset InkDialogue;
    public AK.Wwise.Event errorSound;

    [field:SerializeField] public bool isRepeatable { get; set; } = true;
    

    public void LoadDialogue()
    {
        if(isRepeatable)
        {
            Debug.Log("Loading Dialogue...");
            SendInkFile.Invoke(InkDialogue);
        }
        else
        {
            Debug.Log("Cannot use console again.");
            errorSound.Post(gameObject);
        }
        
    }

    public void UnLoadDialogue()
    {
       
    }

    void Awake()
    {
        name = gameObject.name;
    }
}
