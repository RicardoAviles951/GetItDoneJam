using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public bool onlyRunStartFunction; // for Noah to check the laser issue


    public float radius = 15f;
    public float fixRadiusIndicator = 1.5f;
    public float rangeIndicatorHeight;
    public float rotationSpeed;
    public float moveSpeed;
    public float stopDistance;
    public float laserDmage;
    public float speed;
    public float attackSpeed;

    public Rotate rotationAlas;

    public float timeBetweenAttacks;
    private float cooldown;

    private int placeToGo;

    [Range(0, 360)]
    public float angle;

    private float angleStart;

    public string playerTag = "Player";

    public GameObject playerObject;

    public LayerMask playerMask;
    public LayerMask obstaclesMask;
    public PlayerHealth player;



    public bool canSeePlayer;

    [SerializeField] private LineRenderer beam;
    [SerializeField] private Transform firePoint;

    [SerializeField] private ParticleSystem StartFX;
    [SerializeField] private ParticleSystem EndFX;

    public Vector3 offset;

    public GameObject rangeIndicator;

    private void Start()
    {
        speed = rotationAlas.speed;
        beam.enabled = false; 
        cooldown = timeBetweenAttacks;
        angleStart = angle;
        playerObject = GameObject.FindGameObjectWithTag(playerTag);
        player = playerObject.GetComponent<PlayerHealth>();
        rangeIndicator.transform.localScale = new Vector3(radius* fixRadiusIndicator, rangeIndicatorHeight, radius * fixRadiusIndicator);
    }
    private void Update()
    {
        if (GameManager.instance.lostGame || GameManager.instance.wonGame)
        {
            return;
        }

        if (onlyRunStartFunction)
            return;

        FieldOfViewCheck();

        if (!canSeePlayer)
        {
            angle = angleStart;
        }
    }
    public void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, playerMask);

        Collider[] bunrningChecks = Physics.OverlapSphere(transform.position, radius);

        List<Collider> validColliders = new List<Collider>();

        foreach (Collider col in bunrningChecks)
        {
            BurnDestruct burningObject = col.GetComponent< BurnDestruct>();
           // BurningObject burningObject = col.GetComponent<BurningObject>();

            if (burningObject != null && burningObject.isBurning)
            {
                validColliders.Add(col);
            }
        }

        bunrningChecks = validColliders.ToArray();

        if (bunrningChecks != null)
        {
            if (bunrningChecks.Length != 0)
            {
                Transform target = bunrningChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < angle * 0.5f)//Dividido para dor para tener un angulo claro y detallado
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstaclesMask))
                    {
                        canSeePlayer = true;
                        EnemyMovement(target);
                        angle = 360;
                        return;
                    }
                    else
                    {
                        DeactivateLaser();
                        canSeePlayer = false;
                    }
                }
                else
                {
                    DeactivateLaser();
                    canSeePlayer = false;
                }
            }
            else if (canSeePlayer)
            {
                DeactivateLaser();
                canSeePlayer = false;
            }
        }

        if (rangeChecks != null)
        {
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

                        if (player != null)
                        {
                            player.TakeDamage(laserDmage*Time.deltaTime);
                        }
                    }
                    else
                    {
                        DeactivateLaser();
                        canSeePlayer = false;
                    }
                }
                else
                {
                    DeactivateLaser();
                    canSeePlayer = false;
                }
            }
            else if (canSeePlayer)
            {
                DeactivateLaser();
                canSeePlayer = false;
            }
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
            ActivateLaser();

        }
    }

    private void ActivateLaser()
    {
        beam.enabled = true;
        StartFX.Play();
        EndFX.Play();
        rotationAlas.speed = attackSpeed;

        Vector3 directionToPlayer = (playerObject.transform.position - firePoint.position).normalized;

        Ray ray = new Ray(firePoint.position, directionToPlayer);

        bool cast = Physics.Raycast(ray, out RaycastHit hit, radius);

        Vector3 hitPosition = cast ? hit.point : firePoint.position + directionToPlayer * radius;

        beam.SetPosition(0, firePoint.position);
        beam.SetPosition(1, hitPosition);
        EndFX.transform.position = hitPosition - offset;
    }
    private void DeactivateLaser()
    {
        rotationAlas.speed = speed;
        beam.enabled = false;
        beam.SetPosition(0, firePoint.position);
        beam.SetPosition(1, firePoint.position);
        StartFX.Stop();
        EndFX.Stop();
    }

}
