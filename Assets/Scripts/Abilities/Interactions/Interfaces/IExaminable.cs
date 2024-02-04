using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExaminable
{
    void Examine(Vector3 viewPosition);

    void UnExamine(Vector3 originalPosition, Quaternion originalRotation);
}
