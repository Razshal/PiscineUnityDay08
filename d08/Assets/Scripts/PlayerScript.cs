using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : CharacterScript
{
    private RaycastHit clickHit;
    new private Camera camera;

    new void Start()
    {
        base.Start();
        camera = Camera.main;
    }

    new void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0)
            && Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out clickHit))
        {
            // Sets enemy target for mother script
            if (clickHit.collider && clickHit.collider.gameObject.CompareTag("Enemy"))
                enemyTarget = clickHit.collider.gameObject;
            // Or set player click movement
            else
                navMeshAgent.SetDestination(clickHit.point);
        }
    }
}
