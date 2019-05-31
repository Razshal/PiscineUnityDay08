using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.AI;public class PlayerScript : CharacterScript{    private RaycastHit clickHit;    new private Camera camera;    public bool attackFrame = false;    private int lastAttack = 0;    private int frameCount;    new void Start()    {        base.Start();        camera = Camera.main;    }    private void OnTriggerStay(Collider other)
    {
        if (!other.isTrigger && other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<CharacterScript>().isAlive)
            enemyTarget = other.gameObject;
    }    public void AttackEnnemyForAnimation()    {
        if (state == State.ATTACKING && enemyTarget && attackFrame && lastAttack + 5 < frameCount)
        {            lastAttack = frameCount;            enemyTarget.GetComponent<EnemyScript>().ReceiveDamages();
            attackFrame = false;            if (enemyTarget.GetComponent<EnemyScript>().life <= 0)            {
                state = State.IDLE;                enemyTarget = null;
            }
        }
    }    new void Update()    {        base.Update();
        AttackEnnemyForAnimation();        frameCount = Time.frameCount;        if (Input.GetMouseButtonDown(0)            && Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out clickHit))        {
            // Sets enemy target for mother script
            if (clickHit.collider && clickHit.collider.gameObject.CompareTag("Enemy"))                enemyTarget = clickHit.collider.gameObject;
            // Or set player click movement
            else            {                navMeshAgent.SetDestination(clickHit.point);                enemyTarget = null;            }        }    }}