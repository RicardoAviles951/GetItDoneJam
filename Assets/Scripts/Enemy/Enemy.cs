using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float radius = 8f;
    public float rotationSpeed;
    public float moveSpeed;
    public float stopDistance;

    [Range(0,360)]
    public float angle;

    public string playerTag = "Player";

    public GameObject playerObject;

    public LayerMask targetMask;
    public LayerMask obstaclesMask;

    public bool canSeePlayer;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag(playerTag);
    }
    private void Update()
    {
        FieldOfViewCheck();
    }
    public void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle * 0.5f)//Dividido para dor para tener un angulo claro y detallado
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstaclesMask))
                {
                    canSeePlayer = true;
                    enemyMovement(target);
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }

    }

    public void enemyMovement(Transform target)
    {
        if (canSeePlayer)
        {
            //Rotation
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);


            //Movement
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget > stopDistance) // Ajusta el umbral según sea necesario
            {
                transform.Translate(Vector3.forward * Mathf.Min(moveSpeed * Time.deltaTime, distanceToTarget));
            }
        }
    }
}
