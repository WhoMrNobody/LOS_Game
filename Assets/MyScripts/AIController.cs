using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float chaseToDistance = 5f;
    [SerializeField] float suspicionTime = 5f;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 1f;
    [SerializeField] float waypointDwellTime = 3f; // Gezme esnasýnda gözlem süresi

    [Range(0, 1)]
    [SerializeField] float patrolSpeedFraction = 0.2f;

    Fighter fighter;
    Mover mover;
    GameObject player;
    Health health;
    Vector3 guardPosition;
    float timeSinceLastSawPlayer = Mathf.Infinity;
    float timeSinceArrivedAtWaypoint = Mathf.Infinity;
    int currentWaypointIndex = 0;
    void Start()
    {
        fighter = GetComponent<Fighter>();
        health = GetComponent<Health>();
        player = GameObject.FindWithTag("Player");
        guardPosition = transform.position;
        
    }

    
    void Update()
    {
        if (health.IsDead()) return; // Yaratýk ölü ise kodlarý çalýþtýrma

        if(InAttackRangeOfPlayer() && fighter.CanAttack(player))
        {
            AttackBehaviour();

        }else if (timeSinceLastSawPlayer < suspicionTime)
        {
            SuspicionBehaviour();
        }
        else
        {
            PatrolBehaviour();
        }

        UpdateTimers();
    }

    void UpdateTimers()
    {
        timeSinceLastSawPlayer += Time.deltaTime;
        timeSinceArrivedAtWaypoint += Time.deltaTime;
    }

    void PatrolBehaviour()
    {
        Vector3 nextPosition = guardPosition;

        if(patrolPath != null)
        {
            if (AtWaypoint())
            {
                timeSinceArrivedAtWaypoint = 0;
                CycleWaypoint();
            }
            nextPosition = GetCurrentWaypoint();
        }

        if(timeSinceArrivedAtWaypoint < waypointDwellTime)
        {
            mover.StartMoveAction(nextPosition, patrolSpeedFraction);
        }
    }

    void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);   
    }

    bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    void SuspicionBehaviour()
    {
        GetComponent<ActionSchedule>().CancelCurrentAction();
    }

    void AttackBehaviour()
    {
        timeSinceLastSawPlayer = 0;
        fighter.Attack(player);
    }

    bool InAttackRangeOfPlayer()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distanceToPlayer < chaseToDistance; // Kovalama deðerinden küçük ise oyuncuya saldýracak
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseToDistance);
    }
}
