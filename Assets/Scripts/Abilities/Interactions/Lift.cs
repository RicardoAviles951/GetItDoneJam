using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lift : MonoBehaviour
{
    public GameObject raisePoint;
    private Vector3 startingPosition;
    private Vector3 endingPosition;
    private bool isMovingTowardsEnding = false; // Track the direction of movement
    public AK.Wwise.Event elevatorSound;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        endingPosition = raisePoint.transform.position;
    }

    public void ActivateElevator()
    {
        
        if (!isMovingTowardsEnding)
        {
            elevatorSound.Post(gameObject);
            isMovingTowardsEnding = true;
            transform.DOMove(endingPosition, 2.0f).SetEase(Ease.InSine).OnComplete(MoveComplete);
            raisePoint.transform.position = startingPosition;
            Debug.Log("Moving towards ending position");
        }
        else
        {
            elevatorSound.Post(gameObject);
            isMovingTowardsEnding = false;
            transform.DOMove(startingPosition, 2.0f).SetEase(Ease.InSine).OnComplete(MoveComplete);
            raisePoint.transform.position = endingPosition;
            Debug.Log("Moving towards starting position");
        }
    }

    private void MoveComplete()
    {
        // Perform any necessary actions after the elevator movement completes
        elevatorSound.Stop(gameObject);
    }

   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 position = raisePoint.transform.position;
        Vector3 size = new Vector3(.25f,.25f,.25f);

        Gizmos.DrawLine(position, transform.position);
        Gizmos.DrawCube(position, size);
    }

    public void OnTriggerStay(Collider trig)
    {
        if (trig.tag == "Player")
            trig.transform.SetParent(transform);
    }

    public void OnTriggerExit(Collider trig)
    {
        if (trig.tag == "Player")
            trig.transform.SetParent(null);
    }
}
