using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExaminable: IInteractable
{
    bool isGrabbable { get; set;}
    Vector3 originalPosition { get; set;}
    Quaternion originalRotation { get; set;}
    void Examine(Vector3 viewPosition);

    void UnExamine(Vector3 originalPosition, Quaternion originalRotation);

    void ToggleExaminable(bool active, Vector3 position);
}
