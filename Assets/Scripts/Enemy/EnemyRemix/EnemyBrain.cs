using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyBrain : MonoBehaviour
{
    [Header("Searching")]
    public float radius;
    public GameObject rangeIndicator;
    public float fixRadiusIndicator = 1.5f;
    public float rangeIndicatorHeight;

    [Header("Targeting")]
    public float rotationSpeed;
    public float attackSpeed;
    private Transform target;

    [Header("Damaging")]
    public float damage;
    public float timeBetweenAttack;
    private float timer = 0;


    //Components
    private TargetDetector detector;
    private Tracker tracker;
    private LaserController laser;

    //Enemy State
    public enum EnemyState
    {
        Searching,
        AttackPlayer,
        AttackBurnable
    }
    [Header("Enemy State")]
    public EnemyState enemyState = EnemyState.Searching;

    // Start is called before the first frame update
    void Start()
    {
        detector = GetComponent<TargetDetector>();
        tracker  = GetComponent<Tracker>();
        laser    = GetComponent<LaserController>();

        rangeIndicator.transform.localScale = new Vector3(radius * fixRadiusIndicator, rangeIndicatorHeight, radius * fixRadiusIndicator);
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyState)
        {
            case EnemyState.Searching:
                Search();
                break;
            case EnemyState.AttackPlayer:
                AttackPlayer();
                Search();
                break;
            case EnemyState.AttackBurnable:
                AttackBurnable();
                break;
        }
    }

    void Search()
    {
       Collider[] player = detector.DetectPlayer(radius);
       Collider[] burnables= detector.DetectBurnable(radius);

        if (detector.playerDetected && detector.burnableDetected)//IF player and burnable in range
        {
            //Prioritize burnable
            target = burnables[0].transform;
            enemyState = EnemyState.AttackBurnable;
        }
        else if(detector.burnableDetected)//IF burnable in range
        {
            target = burnables[0].transform;
            enemyState = EnemyState.AttackBurnable;
        }
        else if(detector.playerDetected)//If player detected
        {
            if (target == null)
            {
                target = player[0].transform;
                
                enemyState = EnemyState.AttackPlayer;
                Debug.Log("Target counter");
            }
        }
        else
        {
            if(enemyState != EnemyState.Searching)
            {
                enemyState = EnemyState.Searching;
            }
            laser.DeactivateLaser(2);
            target = null;
        }
    }

    void AttackPlayer()
    {
        if(target != null)
        {
            laser.ActivateLaser(10, target.position, radius);
            // Track target
            PlayerHealth health = target.GetComponent<PlayerHealth>();
            tracker.TrackTarget(target, rotationSpeed);

            //Damage the player in intervals
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
                else
                {
                    Debug.Log("No health detected");
                }
                timer = timeBetweenAttack;
            }

        }
        
    }

    void AttackBurnable()
    {
        
        if(target != null)
        {
            IBurnable burnable = target.GetComponent<IBurnable>();
            tracker.TrackTarget(target, rotationSpeed);
            laser.ActivateLaser(10, target.position, radius);
            if(burnable != null && !burnable.isBurning)
            {
                laser.DeactivateLaser(5);
                target = null;
                enemyState = EnemyState.Searching;
            }
        }
        else
        {
            enemyState = EnemyState.Searching;
        }
        
    }

    
}
