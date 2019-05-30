using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : CharacterScript
{
    private GameObject player;

    new void Start()
    {
        base.Start();
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
        }
	}

	private void FixedUpdate()
	{

	}

	new void Update()
    {
        base.Update();
        if (player)
            navMeshAgent.SetDestination(player.transform.position);
    }
}
