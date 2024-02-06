using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isOpen = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void ToggleDoor()
    {
        if (isOpen)
        {
            anim.SetBool("Open", false); 
            isOpen = false;
        }
        else
        {
            anim.SetBool("Open", true);
            isOpen = true;
        }
    }
}
