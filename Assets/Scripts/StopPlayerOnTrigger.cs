using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StopPlayerOnTrigger : MonoBehaviour
{
    public UnityEvent linkedEvent;
    private PlayerStateManager psm; // die()

    public void OnTriggerEnter(Collider trig)
    {
        if (trig.tag == "Player")
            linkedEvent.Invoke();
    }
}
