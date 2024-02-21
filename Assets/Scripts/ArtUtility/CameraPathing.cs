using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para> Intended to take a list of transforms move / rotate to each at a constant rate </para>
/// </summary>
public class CameraPathing : MonoBehaviour
{
    public List<Transform> allPositions = new List<Transform>();
    public int targetID = 0;
    private Transform target;
    public float speedMove = 4;
    public float speedRotate = 0.75f;

    public bool pause;

    private void Start()
    {
        target = allPositions[targetID];
    }

    void Update()
    {

        if (pause)
            return;

        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, speedRotate * Time.deltaTime);

        var step = speedMove * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target.position) < 0.001f)
        {
            // pick next target
            if (targetID < allPositions.Count)
                targetID++;            
            else
                pause = true;

            target = allPositions[targetID];
        }

    }// end of Update()


}// end of CameraPathing class
