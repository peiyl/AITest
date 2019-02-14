using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSystemManager : MonoBehaviour {

    //初始化当前感知器列表
    List<Sensor> currentSensors = new List<Sensor>();
    //初始化当前触发器列表
    List<Trigger> currentTriggers = new List<Trigger>();
    //记录当前时刻需要被移除的感知器，例如感知体死亡，需要移除感知器时
    List<Sensor> sensorsToRemove;
    //记录当前时刻需要被移除的触发器，例如触发器已过时；
    List<Trigger> triggersToRemove;

	void Start () {
        sensorsToRemove = new List<Sensor>();
        triggersToRemove = new List<Trigger>();
	}
    private void UpdateTriggers()
    {
        //对于当前触发器列表中的每个触发器t
        foreach (Trigger t in currentTriggers)
        {
            //如果t需要被移除
            if (t.toBeRemoved)
            {
                //将t加入需要移除的触发器列表中
                triggersToRemove.Add(t);
            }
            else
            {
                //更新触发器内部信息
                t.Updateme();
            }
        }
        foreach (Trigger t in triggersToRemove)
        {
            currentTriggers.Remove(t);
        }
    }
    private void TryTriggers()
    {
        //对于当前感知器列表中的每个感知器S
        foreach (Sensor s in currentSensors)
        {
            //如果s所对应的感知体还存在
            if (s.gameObject != null)
            {
                //对于当前触发器列表中的每个触发器t
                foreach (Trigger t in currentTriggers)
                {
                    //检查s是否在t的作用范围内，并且做出相应的响应；
                    t.Try(s);
                }
            }
            else
            {
                //将感知器S加入到粗腰移除的感知器列表中
                sensorsToRemove.Add(s);
            }
        }
        //对于需要移除的感知器列表中的每个感知器s，从当前感知器列表中移除s
        foreach (Sensor s in sensorsToRemove)
        {
            currentSensors.Remove(s);
        }
    }
	void Update () {
        //更新所有触发器内部状态
        UpdateTriggers();
        //迭代所有感知器和触发器，做出相应的行为
        TryTriggers();
	}
    //用于注册触发器
    public void RegisterTrigger(Trigger t)
    {
        print("registering trigger:" + t.name);
        //将参数触发器t加入到当前触发器列表中
        currentTriggers.Add(t);
    }
    //用于注册感知器
    public void RegisterSensor(Sensor s)
    {
        print("registering sensor:" + s.name + s.sensorType);
        //将参数感知器加入到当前感知器列表中
        currentSensors.Add(s);
    }
}
