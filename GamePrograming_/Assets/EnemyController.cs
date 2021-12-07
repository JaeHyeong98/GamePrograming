using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent meshAgent;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform character;
    public Transform target;
    private bool isArrive = false;
    public LayerMask targetMask;

    private void Awake()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        animator = character.GetComponent<Animator>();
        animator.SetLayerWeight(1, 1f);
    }

    private void FixedUpdate()
    {
        if(target)
        {
            meshAgent.SetDestination(target.position);
            animator.SetFloat("Speed", meshAgent.speed);
        }

        isArrive = Physics.CheckSphere(transform.position, meshAgent.stoppingDistance, targetMask);

        if (isArrive) animator.SetFloat("Speed", 0f);
    }
}
