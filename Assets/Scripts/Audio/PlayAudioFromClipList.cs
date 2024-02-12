using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>Call a function to play one of multiple audio clips</para>
/// </summary>
public class PlayAudioFromClipList : MonoBehaviour
{
    [Tooltip("The Audiosource we want to play from, if we dont pick one the script will add one")]
    public AudioSource aSource;
    [Tooltip("The volume our script will set the audisource to, (the clips play based on the audiosource volume || and the audiosource volume is set by this script)")]
    [Range(0,1)] public float clipVolume = 0.3f;
    [Tooltip("The List of audio clips we want to randomly pick from")]
    public List<AudioClip> listOfClips = new List<AudioClip>();
    private AudioClip lastClipPlayed;
    [Tooltip("The amount of time required to pass before another clip can be played")]
    public float timeBetweenClips = 0.4f; // 0.4f is good for fixed update calling
    private float timeBetweenStamp;
        

    public void PlayRandomClip()
    {
        if (listOfClips.Count == 0 || Time.time < timeBetweenStamp + timeBetweenClips)
            return;

        if (!aSource)
            aSource = gameObject.AddComponent<AudioSource>();
        aSource.volume = clipVolume;

        int randomID = Random.Range(0, listOfClips.Count);
        if(listOfClips[randomID] == lastClipPlayed)
        {
            //print("played this before");
            PlayRandomClip();            
            return;
        }
        aSource.clip = listOfClips[randomID];
        aSource.PlayOneShot(listOfClips[randomID], aSource.volume);
        lastClipPlayed = listOfClips[randomID];
        timeBetweenStamp = Time.time;
        //print("finished playing audio");

    }// end of PlayRandomClip()



}// end of PlayAudioFromClipList class
