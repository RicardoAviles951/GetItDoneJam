using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para> Inteaded to fade lights as triggered </para>
/// </summary>
public class Light_URP_Fade : MonoBehaviour
{

    public bool happensOnce, disableOnFinished, startVisible;
    private bool triggeredOnce, runningNow;

    public Light lightRef;
    public Vector2 startEndIntensity;
    public float amountChange;
    public float smoothTime;

    private void OnEnable()
    {
        triggeredOnce = false;
        lightRef.intensity = startEndIntensity.x;
        lightRef.enabled = startVisible;
    }

    private void OnTriggerEnter(Collider trig)
    {
        if (happensOnce && triggeredOnce || runningNow)
            return;

        if (trig.tag == "Player")
        {
            lightRef.enabled = true;
            StartCoroutine(FadeLight(smoothTime));
            triggeredOnce = true;
        }
    }


    public IEnumerator FadeLight(float _smoothTime)
    {
        runningNow = true;

        if (lightRef.intensity > startEndIntensity.y + amountChange)
            lightRef.intensity -= amountChange;
        if (lightRef.intensity < startEndIntensity.y + amountChange)
            lightRef.intensity += amountChange;

        yield return new WaitForSeconds(_smoothTime);

        if (lightRef.intensity > startEndIntensity.y + amountChange || lightRef.intensity < startEndIntensity.y + amountChange)
            StartCoroutine(FadeLight(_smoothTime));
        else
        {
            lightRef.enabled = disableOnFinished;
            runningNow = false;
        }

    }


}// end of Light_URP_Fade class
