using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiChasePlayerState : AiState
{
    float timer = 0.0f;
    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }
    public void Enter(AiAgent agent)
    {
       
    }

    public void Update(AiAgent agent)
    {
        if (!agent.enabled) return;

        timer -= Time.deltaTime;

        if (timer < 0.0f)
        {
            float sqDistance = (agent.playerTransform.position - agent.navMeshAgent.destination).sqrMagnitude;

            if (sqDistance > agent.config.maxDistance * agent.config.maxDistance)
            {
                agent.navMeshAgent.destination = agent.playerTransform.position;
            }
            timer = agent.config.maxTime;
        }
    }
    public void Exit(AiAgent agent)
    {
        
    }
    
}
