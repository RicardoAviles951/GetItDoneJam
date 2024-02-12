using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    public void PlayVoiceLine(AK.Wwise.Event voiceLine)
    {
        voiceLine.Post(gameObject);
    }

    private void OnEnable()
    {
        TriggerVoiceLine.SendVoiceLine += PlayVoiceLine;
    }

    private void OnDisable()
    {
        TriggerVoiceLine.SendVoiceLine -= PlayVoiceLine;
    }


}
