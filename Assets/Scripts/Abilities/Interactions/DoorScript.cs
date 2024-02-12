using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isOpen, isUnlocked = false;
    private Animator anim;
    public AK.Wwise.Event doorSound;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void ToggleDoor()
    {
        anim.SetBool("Open", isOpen);
    }

    public void UnlockDoor(bool _unlcoked)
    {
        isUnlocked = _unlcoked;
    }

    private void OnTriggerStay(Collider trig)
    {
        if (!isUnlocked)
            return;

        if (trig.tag == "Player")
        {
            if (!isOpen)
            {
                doorSound.Post(gameObject);
                isOpen = true;
            }
            
            ToggleDoor();
        }
    }

    private void OnTriggerExit(Collider trig)
    {
        if (!isUnlocked)
            return;

        if (trig.tag == "Player")
        {
            if (isOpen)
            {
                doorSound.Post(gameObject);
                isOpen = false;
            }
            
            ToggleDoor();
        }
    }
}
