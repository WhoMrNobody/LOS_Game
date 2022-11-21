using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] float playerMoveForwardSpeed;
    [SerializeField] float movementSpeed;
    [SerializeField] float maxSpeed;

    [HideInInspector]
    public Animator animator;
    NavMeshAgent navMeshAgent;
    void Start()
    {
        animator = GetComponent<Animator>(); 
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimator();

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * playerMoveForwardSpeed * Time.deltaTime);
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(movementSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * playerMoveForwardSpeed * Time.deltaTime;

        }else if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * playerMoveForwardSpeed * Time.deltaTime;

        }else if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * playerMoveForwardSpeed * Time.deltaTime;
        }
    }


    void UpdateAnimator()
    {

        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        movementSpeed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forward_Speed", movementSpeed);
    }
}
