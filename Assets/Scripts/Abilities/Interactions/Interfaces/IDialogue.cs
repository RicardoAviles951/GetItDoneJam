using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogue: IInteractable
{
    string name { get; set; }
    bool isRepeatable { get; set; }
    bool CheckForRepeatable();
    bool HasDialogue();
    void LoadDialogue();
   
}
