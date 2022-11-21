using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    public Transform playerTransform;
    public AiSensor sensor;
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        sensor = GetComponent<AiSensor>();
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiPatrol());
        //stateMachine.RegisterState(new AiFindWeaponState());
        stateMachine.ChangeState(initialState);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        stateMachine.Update();
    }
}
