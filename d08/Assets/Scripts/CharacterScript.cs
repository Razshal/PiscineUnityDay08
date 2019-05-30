using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterScript : MonoBehaviour {
    protected Animator animator;
    protected NavMeshAgent navMeshAgent;
    public int maxLife;
    public int life;
    private bool isPlayer;
    protected bool isInContact;

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
        isPlayer = gameObject.CompareTag("Player");
    }

    protected void UpdateAnimation()
    {
        switch (state)
        {
            case State.RUN:
                animator.SetBool("Run", true);
                break;
            case State.ATTACKING:
                animator.SetBool(isPlayer ? "Attack" : "ZombieAtack", true);
                break;
            default:
                animator.SetBool("Run", false);
                animator.SetBool("Attack", false);
                break;
        }
    }

    protected void Update()
    {
        isInContact = navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance;

        if (navMeshAgent.hasPath && !isInContact)
            state = State.RUN;
        else
            state = State.IDLE;
        UpdateAnimation();
    }
}
