using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour {
    new private Camera camera;
    private NavMeshAgent navMeshAgent;
    private RaycastHit clickHit;
    private Animator animator;

    public enum State
    {
        RUN,
        ATTACKING,
        IDLE,
        DEAD
    }

    public State state;


	// Use this for initialization
	void Start () {
        camera = Camera.main;
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		
	}

    private void UpdateAnimation()
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

	// Update is called once per frame
	void Update () {

        UpdateAnimation();
        if (Input.GetMouseButtonDown(0) 
            && Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out clickHit))
            navMeshAgent.SetDestination(clickHit.point);
        if (navMeshAgent.hasPath && navMeshAgent.remainingDistance > 0.1f)
            state = State.RUN;
        else
            state = State.IDLE;
            
	}
}
