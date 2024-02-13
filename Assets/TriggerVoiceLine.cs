using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class TriggerVoiceLine : MonoBehaviour
{
    public static event Action<AK.Wwise.Event> SendVoiceLine;
    public UnityEvent endVOCallback;
    public List<AK.Wwise.Event> VoiceLine;
    public bool linePlayed = false;
    public float timeBetweenLines = 4f;
    //public AK.Wwise.Event VoiceLine;


    public void TriggerVO()
    {

        DetermineVOExecution();
    }


    private IEnumerator PlayVoiceLines()
    {
        for (int i = 0; i < VoiceLine.Count; i++)
        {
            // Check if the voice line is not null
            if (!linePlayed)
            {
                // Invoke the event to play the voice line
                SendVoiceLine?.Invoke(VoiceLine[i]);
                linePlayed = true;

                // Wait until the current voice line finishes playing
                yield return new WaitForSeconds(timeBetweenLines);

                // If this is the last voice line, set linePlayed to true
                if (i == VoiceLine.Count - 1)
                {
                    Debug.Log("VO finished");
                    endVOCallback.Invoke();
                    linePlayed = true;

                }
                else
                {
                    linePlayed = false;
                }
            }
        }
    }

    private void PlaySingleLine()
    {
        if (VoiceLine.Count > 0 && VoiceLine[0] != null)
        {
            // Check if the voice line has not played
            if (!linePlayed)
            {
                // Invoke the event to play the voice line
                SendVoiceLine?.Invoke(VoiceLine[0]);
                linePlayed = true;
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            DetermineVOExecution();
        }
    }

    void DetermineVOExecution()
    {
        if(VoiceLine.Count > 1)
        {
            Debug.Log("Multiple lines");
            StartCoroutine(PlayVoiceLines());
        }
        else
        {
            Debug.Log("Single Line");
            PlaySingleLine();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 pos = transform.position;
        Gizmos.DrawCube(pos, Vector3.one);
    }
}
