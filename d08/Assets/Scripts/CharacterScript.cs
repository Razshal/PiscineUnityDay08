﻿using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.AI;public class CharacterScript : MonoBehaviour
{    protected Animator animator;    protected NavMeshAgent navMeshAgent;    public int maxLife = 3;    public int life;    private bool isPlayer;    public bool isInContact;    public GameObject enemyTarget;    private bool deadTrigger = false;    public bool isAlive;
    // Allows the player to get out of fight
    protected bool prioritaryWaypoint = false;    public enum State    {        RUN,        ATTACKING,        IDLE,        DEAD    }    public State state;    protected void Start()    {        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();        animator = gameObject.GetComponent<Animator>();        isPlayer = gameObject.CompareTag("Player");        life = maxLife;    }    protected void UpdateAnimation()    {        string attackVar = isPlayer ? "Attack" : "ZombieAttack";        switch (state)        {            case State.DEAD:                animator.SetBool(attackVar, false);                animator.SetBool("Run", false);                if (!deadTrigger)                {
                    animator.SetTrigger("Dead");                    deadTrigger = true;                }                break;            case State.RUN:                animator.SetBool(attackVar, false);                animator.SetBool("Run", true);                break;            case State.ATTACKING:                animator.SetBool("Run", false);                animator.SetBool(attackVar, true);                break;            default:                animator.SetBool("Run", false);                animator.SetBool(attackVar, false);                break;        }    }    public void ReceiveDamages()    {        life -= 1;        if (life <= 0)            state = State.DEAD;    }    private IEnumerator DeadDisapearing()    {        yield return new WaitForSeconds(2);        Destroy(gameObject, 2f);        while (gameObject)        {
            transform.Translate(Vector3.down * 0.01f);
            yield return new WaitForEndOfFrame();
        }    }    protected void Update()    {        isAlive = life > 0;        if (state != State.DEAD)
        {            if (enemyTarget)                navMeshAgent.SetDestination(enemyTarget.transform.position);

            // Defines if target is reached
            isInContact = navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;

            // Sets states for animations and attack
            if (navMeshAgent.hasPath && !isInContact)                state = State.RUN;            else if (enemyTarget && isInContact && enemyTarget.GetComponent<CharacterScript>().isAlive)            {                transform.LookAt(enemyTarget.transform.position);                state = State.ATTACKING;            }            else            {
                state = State.IDLE;                prioritaryWaypoint = false;            }        }        else        {            StartCoroutine("DeadDisapearing");        }
        UpdateAnimation();    }}