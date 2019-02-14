using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerMoveByNav : MonoBehaviour {
    private Ray ray;
    private RaycastHit hit;
    private NavMeshAgent navMeshAgent;

	void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();
	}
	void Update () {
        MoveByNav();
	}
    void MoveByNav()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit)&&hit.collider.tag == "Ground")
            {
                navMeshAgent.SetDestination(hit.point);
            }
        }
    }
}
