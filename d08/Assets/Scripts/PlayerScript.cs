using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.AI;public class PlayerScript : CharacterScript{    private RaycastHit clickHit;    new private Camera camera;    public bool attackFrame = false;    new void Start()    {        base.Start();        camera = Camera.main;    }    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.gameObject.CompareTag("Enemy"))
            enemyTarget = other.gameObject;
    }    public void AttackEnnemyForAnimation()    {
        if (state == State.ATTACKING && enemyTarget && attackFrame)
        {
            enemyTarget.GetComponent<EnemyScript>().ReceiveDamages();
            attackFrame = false;            if (enemyTarget.GetComponent<EnemyScript>().life < 0)            {
                state = State.IDLE;
                enemyTarget = null;
            }
        }
    }    new void Update()    {        base.Update();
        AttackEnnemyForAnimation();        if (Input.GetMouseButtonDown(0)            && Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out clickHit))        {
            // Sets enemy target for mother script
            if (clickHit.collider && clickHit.collider.gameObject.CompareTag("Enemy"))                enemyTarget = clickHit.collider.gameObject;
            // Or set player click movement
            else            {                navMeshAgent.SetDestination(clickHit.point);                enemyTarget = null;            }        }    }}