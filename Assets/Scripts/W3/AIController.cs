using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIController : MonoBehaviour {

    private NavMeshAgent navMeshAgent;

    public int health;
    public float arriveDistance = 1.0f;
    //巡逻的路点
    public Transform patrolWayPoints;
    
    //可以停止追逐开始射击的距离
    public float shootingDistance = 7.0f;
    //从射击状态重新转换到追逐状态的距离；
    public float chasingDistance = 8.0f;

    //黑板对象
    private Blackboard bb;
    //当前路点索引
    private int wayPointIndex = 0;
    //最近感知到玩家的位置
    private Vector3 personalLastSighting;
    //上次的玩家位置
    private Vector3 previousSighting;
    //路点的数组
    private Vector3[] wayPoints;
    //记忆对象
    private SenseMemory memory;
    public enum FSMState
    {
        Patrolling = 0,//巡逻
        Chasing,//追逐
        Shooting,//射击
    }
    private FSMState state;
	void Start () {
        health = 30;

        navMeshAgent = GetComponent<NavMeshAgent>();

        //动画
        //黑板
        bb = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<Blackboard>();
        personalLastSighting = bb.resetPosition;
        previousSighting = bb.resetPosition;
        //获得记忆对象
        memory = GetComponent<SenseMemory>();
      
        state = FSMState.Patrolling;
        //保存所有路点到一个数组中；
        wayPoints = new Vector3[patrolWayPoints.childCount];
        int c = 0;
        foreach (Transform item in patrolWayPoints)
        {
            wayPoints[c] = item.position;
            c++;
        }
        navMeshAgent.SetDestination(wayPoints[0]);
	}
	bool CanSeePlayer()
    {
        //如果玩家还在记忆中
        if (memory != null)
            return memory.FindInList();
        else
            return false;
    }
    /// <summary>
    /// 射击
    /// </summary>
    void Shooting()
    {
        state = FSMState.Shooting;
        //射击时站立不动
        navMeshAgent.SetDestination(transform.position);
        //射击动画
        //如果玩家位置被重置，即每个士兵都看不到玩家，那么重新进入巡逻状态
        if (personalLastSighting == bb.resetPosition)
        {
            state = FSMState.Patrolling;
        }
        //如果玩家位置更新，可以再次开始追逐
        if ((personalLastSighting != previousSighting)&&Vector3.Distance(transform.position,personalLastSighting)>chasingDistance)
        {
            Debug.Log("change to chasing again...........");
            state = FSMState.Chasing;
        }
    }
    /// <summary>
    /// 追逐
    /// </summary>
    void Chasing()
    {
        state = FSMState.Chasing;
        //ai目标
        navMeshAgent.SetDestination(personalLastSighting);
        //如果距离玩家很近并且能看见玩家，则转射击，否则继续追逐
        if ((Vector3.Distance(transform.position, personalLastSighting) < shootingDistance) && CanSeePlayer())
            state = FSMState.Shooting;
        else if (!CanSeePlayer())
            state = FSMState.Patrolling;
        //动画
    }
    /// <summary>
    /// 巡逻
    /// </summary>
    void Patrolling()
    {
        state = FSMState.Patrolling;
        //循环寻路
        if (Mathf.Abs(navMeshAgent.remainingDistance) <=0.1f)
        {
            navMeshAgent.SetDestination(wayPoints[wayPointIndex]);
            if (wayPointIndex == wayPoints.Length - 1)
                wayPointIndex = 0;
            else
                wayPointIndex++;

        }
        //如果某个AI士兵看到玩家，进入追逐状态
        if(personalLastSighting != bb.resetPosition)
            state = FSMState.Chasing;
    }
	void Update () {
        //如果玩家位置发生变化，更新
        if (bb.playerLastPosition !=previousSighting)
        {
            personalLastSighting = bb.playerLastPosition;
        }
        switch(state)
        {
            case FSMState.Patrolling:
                Patrolling();
                break;
            case FSMState.Chasing:
                Chasing();
                break;
            case FSMState.Shooting:
                Shooting();
                break;
        }
        previousSighting = bb.playerLastPosition;
        Debug.Log(state);
	}
}
