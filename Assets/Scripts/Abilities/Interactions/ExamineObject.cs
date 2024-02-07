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

    public TextAsset itemDescription;
    private Story currentStory;
    

    private List<string> lines = new List<string>();

    void Start()
    {
        if(itemDescription != null)
        {
            currentStory = new Story(itemDescription.text);
            StoreLines();
        }
        
        
    }

        public void Examine(Vector3 viewPosition)
    {
        //pos.position = viewPosition;
        transform.DOMove(viewPosition,.25f)
            .SetEase(Ease.OutBack);
        if(itemDescription != null)
        {
            itemInfo?.Invoke(new ItemInfo(lines[0], lines[1]));
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
        
        lines.Add(currentStory.Continue());
        lines.Add(currentStory.Continue());

        foreach(string line in lines)
        {
            Debug.Log("Line: " + line);
        }
    }


    
    
}
