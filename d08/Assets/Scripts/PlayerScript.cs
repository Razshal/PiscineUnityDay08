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
			navMeshAgent.SetDestination(clickHit.point);         
        }
    }
}
