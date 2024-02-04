using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerPrompt : MonoBehaviour
{
    public static UnityAction triggerPrompt;
    public static UnityAction triggerNoPrompt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        triggerPrompt?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        triggerNoPrompt?.Invoke();
    }
}
