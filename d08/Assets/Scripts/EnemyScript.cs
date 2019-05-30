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
            player = other.gameObject;
    }

    new void Update()
    {
        base.Update();
        if (player)
            navMeshAgent.SetDestination(player.transform.position);
        if (isInContact && player)
        {
            state = State.ATTACKING;
        }
    }
}
