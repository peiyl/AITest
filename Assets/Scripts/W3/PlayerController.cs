using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
    private NavMeshAgent navMeshAgent;
    private Ray ray;
    private RaycastHit hit;
    void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();
	}
	void Update () {
        PlayMoveByNav();
	}
    void PlayMoveByNav()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Ground")
            {
                navMeshAgent.SetDestination(hit.point);
            }
        }
    }
}
