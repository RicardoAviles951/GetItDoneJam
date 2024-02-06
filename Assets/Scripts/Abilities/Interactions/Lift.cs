using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lift : MonoBehaviour
{
    private Animator animator;
    public GameObject raisePoint;
    private Vector3 startingPosition;
    private Vector3 endingPosition;
    private bool isMovingTowardsEnding = false; // Track the direction of movement

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startingPosition = transform.position;
        endingPosition = raisePoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateElevator()
    {
        
        if (!isMovingTowardsEnding)
        {
            isMovingTowardsEnding = true;
            transform.DOMove(endingPosition, 2.0f).OnComplete(MoveComplete);
            raisePoint.transform.position = startingPosition;
            Debug.Log("Moving towards ending position");
        }
        else
        {
            isMovingTowardsEnding = false;
            transform.DOMove(startingPosition, 2.0f).OnComplete(MoveComplete);
            raisePoint.transform.position = endingPosition;
            Debug.Log("Moving towards starting position");
        }
    }

    private void MoveComplete()
    {
        // Perform any necessary actions after the elevator movement completes
    }

   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 position = raisePoint.transform.position;
        Vector3 size = new Vector3(.25f,.25f,.25f);

        Gizmos.DrawLine(position, transform.position);
        Gizmos.DrawCube(position, size);
    }
}
