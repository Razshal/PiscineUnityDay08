using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterScript : MonoBehaviour {
    protected Animator animator;
    protected NavMeshAgent navMeshAgent;


    public enum State
    {
        RUN,
        ATTACKING,
        IDLE,
        DEAD
    }
    public State state;

    protected void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
    }

    protected void UpdateAnimation()
    {
        switch (state)
        {
            case State.RUN:
                animator.SetBool("Run", true);
                break;
            case State.ATTACKING:
                animator.SetBool("Attack", true);
                break;
            default:
                animator.SetBool("Run", false);
                animator.SetBool("Attack", false);
                break;
        }
    }

    protected void Update()
    {
        if (navMeshAgent.hasPath && navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
            state = State.RUN;
        else
            state = State.IDLE;
        UpdateAnimation();
    }
}
