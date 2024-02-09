using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacer : MonoBehaviour, IPlacer
{
    public List<Transform> transformList = new List<Transform>();
    public int itemCount = 0;

    public void PlaceItem(IExaminable i)
    {
        //Assuming we have enough space then execute code
        if(itemCount < transformList.Count)
        {
            //Put it in one of the positions
            i.ToggleExaminable(true, transformList[itemCount].position);
            //set flag so the object cannot be interacted with again. 
            i.isGrabbable = false;

            itemCount++;
        }
        else //Put the extra object back where it was originally
        {
            i.ToggleExaminable(true, i.originalPosition);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get reference to the position objects
        for(int i = 0; i < 5; i++)
        {
            string name = ("Pos" + i); //Position objects are named Pos1, Pos2, Pos3...
            GameObject pos = GameObject.Find(name);
            transformList.Add(pos.transform);
        }
        
    }

    //Thought about making the items spin slowly but it wasn't as straight forward...maybe next time. 
    void ItemSpin()
    {
        foreach(Transform t in transformList)
        {
            // Tween the rotation of the transform indefinitely
            t.DORotate(new Vector3(0f, 360f, 0f), 2f, RotateMode.LocalAxisAdd)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
        }
        
    }
}
