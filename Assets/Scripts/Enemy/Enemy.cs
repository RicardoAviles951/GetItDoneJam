using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float radius = 8f;
    public float rotationSpeed;
    public float moveSpeed;
    public float stopDistance;

    public float timeBetweenAttacks;
    private float cooldown;
    public Transform firePoint;
    public GameObject projectile;

    private int placeToGo;

    [Range(0,360)]
    public float angle;

    public string playerTag = "Player";

    public GameObject playerObject;

    public LayerMask targetMask;
    public LayerMask obstaclesMask;

    public bool canSeePlayer;

    private void Start()
    {
        cooldown = timeBetweenAttacks;
        playerObject = GameObject.FindGameObjectWithTag(playerTag);
        placeToGo = Random.Range(0, EnemyPath.instance.enemyPath.Count);
    }
    private void Update()
    {
        FieldOfViewCheck();

        if (!canSeePlayer)
        {
            PathRoute();
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


            //Movement
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget > stopDistance)
            {
                transform.Translate(Vector3.forward * Mathf.Min(moveSpeed * Time.deltaTime, distanceToTarget));
            }

            //Attack
            timeBetweenAttacks -= Time.deltaTime;

            if (timeBetweenAttacks <= 0)
            {
                EnemyAttack();
                timeBetweenAttacks = cooldown;
            }

        }
    }

    public void EnemyAttack()
    {
        Instantiate(projectile, firePoint.position, transform.rotation);
    }

    void PathRoute()
    {
        if (!canSeePlayer)
        {
            // Rotation
            Vector3 directionToTarget = EnemyPath.instance.enemyPath[placeToGo].transform.position - transform.position;
            directionToTarget.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);

            // Movement
            float distanceToTarget = Vector3.Distance(transform.position, EnemyPath.instance.enemyPath[placeToGo].transform.position);

            if (distanceToTarget > 3f)
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
            else
            {
                placeToGo = Random.Range(0, EnemyPath.instance.enemyPath.Count);
            }
        }
    }
}
