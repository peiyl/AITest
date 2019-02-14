using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSensor : Sensor {
    //定义感知体的听觉范围
    public float hearingDistance = 30.0f;
    //private AIController controller;
    private Blackboard bb;
    private SenseMemory memoryScript;
	void Start () {
        //controller = GetComponent<AIController>();
        //设置感知器类型为声音感知器
        sensorType = SensorType.sound;
        //向管理器注册这个感知器
        manager.RegisterSensor(this);
        bb = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<Blackboard>();
        memoryScript = GetComponent<SenseMemory>();
	}
    public override void Notify(Trigger t)
    {
        //当感知体能够听到触发器声音时被调用，做出相应行为，这里打印信息，并走向声音的位置
        print("I hear some sound at" + t.gameObject.transform.position + Time.time);
        //
        if (memoryScript != null)
        {
            //添加到记忆中
            memoryScript.AddToList(t.gameObject, 0.66f);
        }
        bb.playerLastPosition = t.gameObject.transform.position;
        bb.lastSensedTime = Time.time;
    }
}
