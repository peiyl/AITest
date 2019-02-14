using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour {
    //最近一次感知到玩家时，玩家的位置
    public Vector3 playerLastPosition;
    //当没有感知到玩家时，设置的位置
    public Vector3 resetPosition;
    //上次更新玩家信息的时间
    public float lastSensedTime = 0;
    //重置玩家位置前等待的时间
    public float resetTime = 1.0f;

	void Start () {
        playerLastPosition = new Vector3(100, 100, 100);
        resetPosition = new Vector3(100, 100, 100);
	}
	
	void Update () {
        //如果距离上次更新玩家的时间超过了resetTime，那么重置玩家位置
        if (Time.time -lastSensedTime>resetTime)
        {
            playerLastPosition = resetPosition;
        }
	}
}
