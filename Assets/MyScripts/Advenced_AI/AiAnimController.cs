using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAnimController : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;
    Animator _animator;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (_navMeshAgent.hasPath)
        {
            _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude); 
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }
    }
}
