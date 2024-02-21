using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    private CinemachineBasicMultiChannelPerlin initialNoise;
    public float intensity;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        initialNoise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Call this method to trigger a camera shake with a specified intensity and duration
    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        CinemachineBasicMultiChannelPerlin noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Generate random offsets for the camera
            float xOffset = Random.Range(-intensity, intensity);
            float yOffset = Random.Range(-intensity, intensity);

            // Apply the offsets to the camera noise
            noise.m_AmplitudeGain = xOffset;
            noise.m_FrequencyGain = yOffset;

            // Increment elapsed time
            elapsed += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Reset camera noise after the shake duration
        noise.m_AmplitudeGain = initialNoise.m_AmplitudeGain;
        noise.m_FrequencyGain = initialNoise.m_FrequencyGain;
    }


}
