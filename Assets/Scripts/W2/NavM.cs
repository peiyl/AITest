using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavM : MonoBehaviour {
    private Ray ray;
    private RaycastHit hit;
    private NavMeshAgent navMeshAgent;
    void Start () {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }
    void Update()
    {
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
