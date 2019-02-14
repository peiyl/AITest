using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForArrive : Steering {
    public bool isPlanar = true;
    public float arrivalDistance = 0.3f;
    public float characterRadius = 1.2f;
    //当与目标小于这个距离时开始减速
    public float slowDowmDistance;
    public GameObject target;
    private Vector3 desiredVelocity;
    private Vehicle myVehicle;
    private float maxSpeed;
	void Start () {
        myVehicle = GetComponent<Vehicle>();
        maxSpeed = myVehicle.maxSpeed;
        isPlanar = myVehicle.isPlanar;
	}
    public override Vector3 Force()
    {
        //计算AI角色与目标之间的距离
        Vector3 toTarget = target.transform.position - transform.position;
        //预期速度
        Vector3 desiredVelocity;
        //返回的操控向量
        Vector3 returnForce;
        if (isPlanar)
        {
            toTarget.y = 0;
        }
        float distance = toTarget.magnitude;
        //如果与目标之间的距离大于所设置的减速半径；
        if (distance <= slowDowmDistance)
        {
            //计算预期速度并返回预期速度与当前速度的差
            desiredVelocity = toTarget - myVehicle.velocity;
            //返回预期速度与当前速度的差
            returnForce = desiredVelocity - myVehicle.velocity;
        }
        else
        {
            desiredVelocity = toTarget.normalized * maxSpeed;
            returnForce = desiredVelocity - myVehicle.velocity;
        }
        return returnForce;
    }
    private void OnDrawGizmos()
    {
        //在目标周围画白色线框
        Gizmos.DrawWireSphere(target.transform.position, slowDowmDistance);
    }
}
