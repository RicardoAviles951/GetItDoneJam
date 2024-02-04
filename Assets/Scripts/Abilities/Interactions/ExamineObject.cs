using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExamineObject: MonoBehaviour, IExaminable
{
    public void Examine(Vector3 viewPosition)
    {
        //pos.position = viewPosition;
        transform.DOMove(viewPosition,.25f)
            .SetEase(Ease.OutBack);
            
    }

    public void UnExamine(Vector3 originalPosition, Quaternion originalRotation)
    {
        Transform pos = gameObject.transform;

        //pos.position = originalPosition;
        transform.DOMove(originalPosition, .25f)
            .SetEase(Ease.InBack);

        pos.rotation = originalRotation;
    }
}
