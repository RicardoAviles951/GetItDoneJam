using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float radius = 15f;
    public float rotationSpeed;
    public float moveSpeed;
    public float stopDistance;

    public float timeBetweenAttacks;
    private float cooldown;
    public Transform firePoint;
    public GameObject projectile;

    private int placeToGo;

    [Range(0, 360)]
    public float angle;

    private float angleStart;

    public string playerTag = "Player";

    public GameObject playerObject;

    public LayerMask targetMask;

    public LayerMask obstaclesMask;

    public bool canSeePlayer;

    private void Start()
    {
        cooldown = timeBetweenAttacks;
        angleStart = angle;
        playerObject = GameObject.FindGameObjectWithTag(playerTag);
        placeToGo = Random.Range(0, EnemyPath.instance.enemyPath.Count);
    }
    private void Update()
    {
        FieldOfViewCheck();

        if (!canSeePlayer)
        {
            angle = angleStart;
        }
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
                    EnemyMovement(target);
                    angle = 360;
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

    public void EnemyMovement(Transform target)
    {
        if (canSeePlayer)
        {
            //Rotation
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);

            //Attack
            timeBetweenAttacks -= Time.deltaTime;

            if (timeBetweenAttacks <= 0)
            {
                timeBetweenAttacks = cooldown;
            }

        }
    }

}
