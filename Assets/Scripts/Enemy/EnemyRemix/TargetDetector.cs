using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    public bool playerDetected = false;
    public bool burnableDetected = false;
    public LayerMask playerMask;
    public LayerMask obstaclesMask;

    public Collider[] DetectPlayer(float radius)
    {
        //Search for player using an overlap sphere
        Collider[] player = Physics.OverlapSphere(transform.position, radius, playerMask);

        //If a player is detected within the sphere
        if(player.Length > 0)
        {
            //Get positions and distances
            Vector3 playerPos = player[0].transform.position;
            var directionToTarget = (playerPos - transform.position).normalized;
            var distanceToTarget = Vector3.Distance(transform.position, playerPos);


            //If there is an obstacle in the way
            if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstaclesMask))
            {
                //Don't attack the player
                playerDetected = false;
            }
            else
            {
                //If no obstacle then attack
                playerDetected = true;
            }
        }
        else
        {
            //If no player detected within the sphere at all then do not attack
            playerDetected = false;
        }

        return player;
    }

    public Collider[] DetectBurnable(float radius)
    {
        //Check for burnables using overlap sphere
        Collider[] burnables = Physics.OverlapSphere(transform.position, radius);

        //Use LINQ query to filter for burning objects
        var validColliders = burnables
            .Where(burn =>
            {
                var burnableComponent = burn.GetComponent<IBurnable>();
                return burnableComponent != null && burnableComponent.isBurning;
            })
            .ToArray();

        if (validColliders.Length > 0 )
        {
            burnableDetected = true;
        }
        else
        {
            burnableDetected= false;
        }

        return validColliders;
    }

    

}
