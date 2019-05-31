using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : CharacterScript
{
    private GameObject player;

    new void Start()
    {
        player = GameObject.FindWithTag("Player");
        agility = agility * (int)(level * 0.15f);
        strength = strength * (int)(level * 0.15f);
        constitution += constitution * (int)(level * 0.15f);
        base.Start();

    }

	private void OnMouseDown()
	{
        player.GetComponent<PlayerScript>().enemyTarget = gameObject;
	}

	private void OnMouseOver()
	{
        player.GetComponent<PlayerScript>().enemyHover = gameObject;
	}

	private void OnMouseExit()
	{
        player.GetComponent<PlayerScript>().enemyHover = null;
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            enemyTarget = other.gameObject;
    }

    new void Update()
    {
        base.Update();
    }
}
