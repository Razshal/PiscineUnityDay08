using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.AI;public class CharacterScript : MonoBehaviour
{    protected Animator animator;    protected NavMeshAgent navMeshAgent;
    public GameObject enemyTarget;    private PlayerScript playerScript;    // Stats    public int life;    public int strength = 10;    public int agility = 10;    public int constitution = 10;    public int armor = 10;

    // Substats
    public int minDamage;    public int maxDamage;

    // Level
    public int level;    public int requieredXp = 100;    public int experience;

    // Coin
    public int money = 0;    // Attack conditions    public bool isInContact;	private bool isPlayer;
	private bool deadTrigger = false;
	private bool deathCoroutine = false;

    // Allows the player to get out of fight
    protected bool prioritaryWaypoint = false;
    public enum State    {        RUN,        ATTACKING,        IDLE,        DEAD    }    public State state;    protected void Start()    {        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();        animator = gameObject.GetComponent<Animator>();        isPlayer = gameObject.CompareTag("Player");        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();        life = constitution * 5;        minDamage = strength / 2;        minDamage = minDamage + 4;        if (!isPlayer)        {			experience = 10;            money = 10;        }    }    protected void UpdateAnimation()    {        string attackVar = isPlayer ? "Attack" : "ZombieAttack";        switch (state)        {            case State.DEAD:                animator.SetBool(attackVar, false);                animator.SetBool("Run", false);                if (!deadTrigger)                {
                    animator.SetTrigger("Dead");                    deadTrigger = true;                }                break;            case State.RUN:                animator.SetBool(attackVar, false);                animator.SetBool("Run", true);                break;            case State.ATTACKING:                animator.SetBool("Run", false);                animator.SetBool(attackVar, true);                break;            default:                animator.SetBool("Run", false);                animator.SetBool(attackVar, false);                break;        }    }    public void ReceiveDamages(int attackerAgility, int attackerMinDamage, int attackerMaxDamage)    {        int hitChance = 75 + attackerAgility - agility;        int baseDamage = Random.value < 0.5 ? attackerMinDamage : attackerMaxDamage;        if (Random.value < hitChance / 10)            life -= baseDamage * (1 - armor / 200);                if (life <= 0)        {			state = State.DEAD;            if (!isPlayer)            {                playerScript.ReceiveExperience(experience);                playerScript.money += money;            }        }    }    private IEnumerator DeadDisapearing()    {        deathCoroutine = true;        Destroy(gameObject.GetComponent<CharacterController>());        Destroy(gameObject.GetComponent<NavMeshAgent>());        yield return new WaitForSeconds(4);        Destroy(gameObject, 2f);        while (gameObject)        {
            transform.Translate(Vector3.down * 0.005f);
            yield return new WaitForFixedUpdate();
        }    }    protected void Update()    {        if (state != State.DEAD)
        {            if (enemyTarget)                navMeshAgent.SetDestination(enemyTarget.transform.position);

            // Defines if target is reached
            isInContact = navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;

            // Sets states for animations and attack
            if (navMeshAgent.hasPath && !isInContact)                state = State.RUN;            else if (enemyTarget && isInContact && enemyTarget.GetComponent<CharacterScript>().state != State.DEAD)            {                transform.LookAt(enemyTarget.transform.position);                state = State.ATTACKING;                prioritaryWaypoint = false;            }            else            {
                state = State.IDLE;                prioritaryWaypoint = false;                if (isPlayer)                    enemyTarget = null;            }        }        else if (!deathCoroutine)            StartCoroutine("DeadDisapearing");

        UpdateAnimation();    }}