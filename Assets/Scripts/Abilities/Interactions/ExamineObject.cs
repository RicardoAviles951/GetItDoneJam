using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Ink.Runtime;
using System.ComponentModel;

public class ExamineObject: MonoBehaviour, IExaminable
{
    public static event Action<ItemInfo> itemInfo;

    //Item description from ink file
    public TextAsset itemDescription;
    
    //Examinable properties 
    public bool isGrabbable { get; set; } = true;
    public Vector3 originalPosition { get; set; }
    public Quaternion originalRotation { get; set; }


    private Story currentStory;
    private Vector3 originalSize;
    private ItemInfo info;
    private List<string> lines = new List<string>();

    

    void Start()
    {
        //If the ink file is attached then load the text. 
        if(itemDescription != null)
        {
            currentStory = new Story(itemDescription.text);
            StoreLines();
            //Create and cache instance of item information class to send to UI.
            info = new ItemInfo(lines[0], lines[1]);
        }
        //Cache starting position and rotation.
        originalSize     = transform.localScale;
        originalPosition = transform.position;
    }

    public void Examine(Vector3 viewPosition)
    {
        //Tween the position smoothly
        transform.DOMove(viewPosition,.25f)
            .SetEase(Ease.OutBack);
        //If the ink file is loaded then send that to the UI. 
        if(itemDescription != null)
        {
            itemInfo?.Invoke(info);
        }
         
    }

    public void UnExamine(Vector3 originalPosition, Quaternion originalRotation)
    {
        Transform pos = gameObject.transform;

        //pos.position = originalPosition;
        transform.DOMove(originalPosition, .25f)
            .SetEase(Ease.InBack);
        lines.Clear();
        pos.rotation = originalRotation;
    }


    void StoreLines()
    {
        //Load the title 
        lines.Add(currentStory.Continue());
        //Load the description
        lines.Add(currentStory.Continue());

        //Prints item information to console for debugging.
        foreach(string line in lines)
        {
            Debug.Log("Line: " + line);
        }
    }

    //Toggles the examinable object's active status. 
    //This is called when storing items in inventory or placing them in the world. 
    public void ToggleExaminable(bool active, Vector3 position)
    {
        if (active)
        {
            TweenSize(1, active);
            gameObject.transform.position = position;
        }
        else
        {
            TweenSize(0.25f, active);
        }
        
    }

    //Play tween animation 
    void TweenSize(float size, bool active)
    {
        transform.DOScale(originalSize * size, .25f).SetEase(Ease.InOutBack).OnComplete(
                () => gameObject.SetActive(active)
            );
    }

    
}
