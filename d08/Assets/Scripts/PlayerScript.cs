﻿using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.AI;public class PlayerScript : CharacterScript{    private RaycastHit clickHit;    new private Camera camera;    private int lastAttack = 0;    private int frameCount;    // Allows the animation to communicate his attack frame    public bool attackFrame = false;    new void Start()    {        base.Start();        camera = Camera.main;    }    private void OnTriggerStay(Collider other)
    {
        if (state != State.ATTACKING            && !enemyTarget            && !other.isTrigger            && other.gameObject.CompareTag("Enemy")
            && other.gameObject.GetComponent<CharacterScript>().state != State.DEAD            && !prioritaryWaypoint)        {
            enemyTarget = other.gameObject;
        }
    }    public void AttackEnnemyForAnimation()    {
        if (state == State.ATTACKING && enemyTarget && attackFrame && lastAttack + 5 < frameCount)
        {            lastAttack = frameCount;            enemyTarget.GetComponent<EnemyScript>()                       .ReceiveDamages(agility, minDamage, maxDamage);
            attackFrame = false;
        }
    }    public void ReceiveExperience(int newXp)    {        experience += newXp;        if (experience > requieredXp)        {            experience -= requieredXp;            level += 1;        }    }    new void Update()    {        base.Update();
        AttackEnnemyForAnimation();        frameCount = Time.frameCount;        if (Input.GetMouseButtonDown(0)            && Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out clickHit))        {
            // Set player click movement            if (!clickHit.collider.gameObject.CompareTag("Enemy"))            {                navMeshAgent.SetDestination(clickHit.point);                prioritaryWaypoint = true;                enemyTarget = null;            }        }    }}