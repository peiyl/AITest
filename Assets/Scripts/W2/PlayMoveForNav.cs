using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayMoveForNav : MonoBehaviour {
    private Ray ray;
    private RaycastHit hit;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
	void Start () {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        PlayMoveByNav();
        IdleOrWalk();
    }
    void PlayMoveByNav()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit) && hit.collider.tag == "Ground")
            {
                navMeshAgent.SetDestination(hit.point);
            }
        }
    }
    private void IdleOrWalk()
    {
        if (Mathf.Abs(navMeshAgent.remainingDistance) <= 0.1f)
        {
            animator.SetBool("walk", false);
        }
        else
        {
            animator.SetBool("walk", true);
        }
    }
}
