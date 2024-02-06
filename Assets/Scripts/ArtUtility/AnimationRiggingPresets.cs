using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para> Handles the different optimal settings for the rigs</para>
/// </summary>
public class AnimationRiggingPresets : MonoBehaviour
{
    public bool allowTestKeys = false;

    [Tooltip("The transforms labelled 'RightHandTagret' & 'LeftHandTarget' that influence ")]
    public Transform target_Righthand, target_Lefthand;
    [Space][Header("Positional Changes\n___________________")][Tooltip("The speed that it takes to move to the new position. Higher is faster")]
    public float timeToMoveHands = 1f;
    [Tooltip("Setting Positions will bake the relative locations the arms will move between when told to")]
    public Vector3 posRest_Righthand, posRest_Lefthand, posFire_Righthand, posFire_Lefthand;
    [Space]
    [Header("Positional Changes\n___________________")][Tooltip("RIGHT NOW THIS DOES NOTHING ||The speed that it takes to move to the new position. Higher is faster")]
    public float timeToRotateHands= 1f;
    [Tooltip("Setting Positions will bake the relative locations the arms will move between when told to")]
    public Quaternion rotRest_Righthand, rotRest_Lefthand, rotFire_Righthand, rotFire_Lefthand;


    private bool moveAHand, moveRightHand, moveToFire;

    // Update is called once per frame
    void Update()
    {
        if (allowTestKeys)
        {
            if (Input.GetKeyDown("r")) // stamp relax position
            {
                posRest_Lefthand = target_Lefthand.position;
                posRest_Righthand = target_Righthand.position;

                rotRest_Lefthand = target_Lefthand.rotation;
                rotRest_Righthand = target_Righthand.rotation;
            }

            if (Input.GetKeyDown("f")) // stamp fire position
            {
                posFire_Lefthand = target_Lefthand.position;
                posFire_Righthand = target_Righthand.position;

                rotFire_Lefthand = target_Lefthand.rotation;
                rotFire_Righthand = target_Righthand.rotation;
            }

            if (Input.GetKeyDown("1")) // move left hand to rest position
                SetLimbTarget(false, false);
            if (Input.GetKeyDown("2")) // move left hand to fire position
                SetLimbTarget(false, true);
            if (Input.GetKeyDown("3")) // move right hand to rest position
                SetLimbTarget(true, false);
            if (Input.GetKeyDown("4")) // move right hand to fire position
                SetLimbTarget(true, true);
        }

        if (moveAHand)
            MoveTowards();

    }// end of Update()

    public void SetLimbTarget(bool _moveRightHand, bool _moveToFire)
    {
        moveRightHand = _moveRightHand;
        moveToFire = _moveToFire;
        moveAHand = true;       
    }// end of SetLimbTarget()

    private void MoveTowards()
    {
        Vector3 targetPos = Vector3.zero;
        Quaternion targetRot = new Quaternion(0,0,0,0);
        Transform handToMove = null;

        if (moveRightHand)
        {
            handToMove = target_Righthand;
            if (moveToFire)
            {
                targetPos = posFire_Righthand;
                targetRot = rotFire_Righthand;
            }
            else
            {
                targetPos = posRest_Righthand;
                targetRot = rotRest_Righthand;
            }
        }
        else
        {
            handToMove = target_Lefthand;
            if (moveToFire)
            {
                targetPos = posFire_Lefthand;
                targetRot = rotFire_Lefthand;
            }
            else
            {
                targetPos = posRest_Lefthand;
                targetRot = rotRest_Lefthand;
            }
        }               

        // Move our position a step closer to the target.
        float step = timeToMoveHands * Time.deltaTime; // calculate distance to move
        if(moveRightHand)
            handToMove.position = Vector3.MoveTowards(handToMove.position, targetPos, step);
        else
            handToMove.position = Vector3.MoveTowards(handToMove.position, targetPos, step);

        handToMove.rotation = targetRot;

        // Check if the positions are approximately equal.
        if (Vector3.Distance(handToMove.position, targetPos) < 0.001f)
        {
            moveAHand = false;
        }

    }// end of MoveTowards()
   

}// end of AnimationRiggingPresets class
