using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPatrol : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Patrol;
    }
    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.speed = agent.config.patrolSpeed;
    }
    public void Update(AiAgent agent)
    {
        //Patrol
        if (!agent.navMeshAgent.hasPath)
        {
            WorldBounds worldBounds = GameObject.FindObjectOfType<WorldBounds>();
            Vector3 min = worldBounds.minBounds.position;
            Vector3 max = worldBounds.maxBounds.position;

            Vector3 randomPosition = new Vector3(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y),
                Random.Range(min.z, max.z)
                );
            agent.navMeshAgent.destination = randomPosition;
        }

        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;

        if (playerDirection.magnitude < agent.config.maxSightDistance)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
        

    }

    public void Exit(AiAgent agent)
    {
        
    }
   
}
