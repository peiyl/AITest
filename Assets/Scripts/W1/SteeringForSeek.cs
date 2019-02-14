using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForSeek : Steering {
    //需要寻找的目标物体
    public GameObject target;
    //预期速度
    private Vector3 desiredVelocity;
    //获得被操纵的AI角色以便查询这个AI角色的最大速度等信息
    private Vehicle myVehicle;
    //最大速度
    private float maxSpeed;
    //是否仅在二维平面上运动
    private bool isPlanar;
	void Start () {
        //获得被操控的AI角色，并读取AI角色允许的最大速度，是否仅在平面上运动
        myVehicle = GetComponent<Vehicle>();
        maxSpeed = myVehicle.maxSpeed;
        isPlanar = myVehicle.isPlanar;
	}
    //计算操控向量
    public override Vector3 Force()
    {
        //计算预期速度
        desiredVelocity = (target.transform.position - transform.position).normalized * maxSpeed;
        if (isPlanar)
        {
            desiredVelocity.y = 0;
        }
        //返回操控向量，即预期速度与当前速度的差；
        return (desiredVelocity-myVehicle.velocity);
    }

    void Update () {
		
	}
}
