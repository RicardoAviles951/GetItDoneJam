using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlacer : IInteractable
{
    void PlaceItem(IExaminable item);
    void PlaySound();
}
