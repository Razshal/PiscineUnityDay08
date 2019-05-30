using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterScript : MonoBehaviour {
    protected Animator animator;
    protected NavMeshAgent navMeshAgent;
    public int maxLife = 3;
    public int life;
    private bool isPlayer;
    public bool isInContact;
    public GameObject enemyTarget;

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
        life = maxLife;
    }

    protected void UpdateAnimation()
    {
        string attackVar = isPlayer ? "Attack" : "ZombieAttack";

        Debug.Log(gameObject.name + " State =" + state);
        switch (state)
        {
            case State.RUN:
                animator.SetBool(attackVar, false);
                animator.SetBool("Run", true);
                break;
            case State.ATTACKING:
                animator.SetBool("Run", false);
                animator.SetBool(attackVar, true);
                break;
            default:
                animator.SetBool("Run", false);
                animator.SetBool(attackVar, false);
                break;
        }
    }

    protected void ReceiveDamages()
    {
        life -= 1;
        if (life <= 0)
            animator.SetTrigger("Dead");
    }

    protected void Update()
    {
        if (enemyTarget)
            navMeshAgent.SetDestination(enemyTarget.transform.position);

        // Defines if target is reached
        isInContact = navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;

        // Sets states for animations and attack
        if (navMeshAgent.hasPath && !isInContact)
            state = State.RUN;
        else if (enemyTarget && isInContact)
            state = State.ATTACKING;
        else
            state = State.IDLE;
        UpdateAnimation();
    }
}
