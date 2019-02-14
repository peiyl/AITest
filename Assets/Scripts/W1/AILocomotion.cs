using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILocomotion :Vehicle
{
    //AI角色的角色控制器
    private CharacterController controller;
    //AI角色每次的移动距离
    private Vector3 moveDistance;
    private void Start()
    {
        //获得角色控制器
        controller = GetComponent<CharacterController>();
        moveDistance = new Vector3(0, 0, 0);
        //调用基类的Start（）函数，进行所需的初始化（不知道什么意思先不写）
    }
    //物理相关操作
    private void FixedUpdate()
    {
        //计算速度
        velocity += acceleration * Time.fixedDeltaTime;
        //限制速度，要低于最大速度
        if (velocity.sqrMagnitude>sqrMaxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
        //计算AI角色的移动距离
        moveDistance = velocity * Time.fixedDeltaTime;
        //如果要求AI角色在平面上移动，那么将Y设置为0；
        if (isPlanar)
        {
            velocity.y = 0;
            moveDistance.y = 0;
        }
        //利用角色控制器使其移动
        controller.SimpleMove(velocity);
        //更新朝向，如果速度大于一个阈值（为了防止抖动）
        if (velocity.sqrMagnitude>0.00001)
        {
            //通过当前朝向与速度方向的插值，计算新的朝向
            Vector3 newForward = Vector3.Slerp(transform.forward, velocity, damping * Time.deltaTime);
            //将y设置为0
            if (isPlanar)
            {
                newForward.y = 0;
            }
            //将向前的方向设置为新的朝向
            transform.forward = newForward;
        }
    }
}

