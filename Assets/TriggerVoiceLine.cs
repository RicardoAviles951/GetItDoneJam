using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class TriggerVoiceLine : MonoBehaviour
{
    public static event Action<AK.Wwise.Event> SendVoiceLine;
    public bool linePlayed = false;
    public AK.Wwise.Event VoiceLine;


    public void TriggerVO()
    {
        if (VoiceLine != null)
        {
            if (!linePlayed)
            {
                SendVoiceLine?.Invoke(VoiceLine);
                linePlayed = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!linePlayed)
            {
                SendVoiceLine?.Invoke(VoiceLine);
                linePlayed= true;
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 pos = transform.position;
        Gizmos.DrawCube(pos, Vector3.one);
    }
}
