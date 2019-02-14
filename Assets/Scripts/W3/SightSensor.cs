using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSensor : Sensor {
    //定义这个ai角色的视域范围
    public float fieldOfView = 45;
    //定义这个角色能看到最远的距离
    public float viewDistance = 100.0f;
    //private AIController controller;
    //黑板对象
    private Blackboard bb;
    //记忆对象
    private SenseMemory memoryScript;
	void Start () {
        //controller = GetComponent<AIController>();
        //设置感知器类型
        sensorType = SensorType.sight;

        manager.RegisterSensor(this);

        bb = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<Blackboard>();
        memoryScript = GetComponent<SenseMemory>();
	}
    public override void Notify(Trigger t)
    {
        /*当感知器能够真正感觉到某个触发器的信息时被调用，
         产生相应的行为或做出某种决策，这里打印出相关信息
         在感知体和触发器之间画一条红色连线，然后角色走向
         看到的物体*/
        print("I see a " + t.gameObject.name + "!");
        Debug.DrawLine(transform.position, t.transform.position, Color.red);
        //controller.MoveToTarget(t.gameObject.transform.position);
        //如果看到的是玩家
        if (t.tag == "Player")
        {
            //在黑板上记录玩家位置和更新时间；
            bb.playerLastPosition = t.gameObject.transform.position;
            bb.lastSensedTime = Time.time;
        }
        if (memoryScript != null)
        {
            //添加到记忆列表中
            memoryScript.AddToList(t.gameObject, 1.0f);
        }
    }
    private void OnDrawGizmos()
    {
        Vector3 frontRayPoint = transform.position + (transform.forward * viewDistance);
        float fieldOfViewinRadians = fieldOfView * 3.14f / 180.0f;
        Vector3 leftRayPoint = transform.TransformPoint(new Vector3(viewDistance * Mathf.Sin(fieldOfViewinRadians), 0, viewDistance * Mathf.Cos(fieldOfViewinRadians)));
        Vector3 rightRayPoint = transform.TransformPoint(new Vector3(-viewDistance * Mathf.Sin(fieldOfViewinRadians), 0, viewDistance * Mathf.Cos(fieldOfViewinRadians)));
        Debug.DrawLine(transform.position,frontRayPoint,Color.green);
        Debug.DrawLine(transform.position, leftRayPoint, Color.green);
        Debug.DrawLine(transform.position, rightRayPoint, Color.green);
    }
}
