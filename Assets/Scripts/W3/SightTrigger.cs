using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightTrigger : Trigger {
    public override void Try(Sensor s)
    {
        //如果感知器能感觉到这个触发器，那么向感知器发出通知，感知体做出相应的决策或行动
        if (isTouchingTrigger(s))
        {
            s.Notify(this);
        }
    }
    //判断感知器是否能感知到这个触发器
    protected override bool isTouchingTrigger(Sensor sensor)
    {
        GameObject g = sensor.gameObject;
        //如果这个感知器能够感知视觉信息
        if (sensor.sensorType == Sensor.SensorType.sight)
        {
            RaycastHit hit;
            Vector3 rayDirection = transform.position - g.transform.position;
            rayDirection.y = 0;
            //判断感知体的向前方向与物体所在方向的夹角，是否在视域范围内；
            if ((Vector3.Angle(rayDirection,g.transform.forward))<(sensor as SightSensor).fieldOfView)
            {
                //在视线距离内是否存在其他障碍物遮挡，如果没用障碍物，则返回true
                if (Physics.Raycast(g.transform.position+new Vector3(0,1,0),rayDirection,out hit,(sensor as SightSensor).viewDistance))
                {
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    /*
     * 更新触发器的内部信息，由于带有视觉触发器的ai角色可能是运动的，
     * 因此要不停更新这个触发器的位置
     */
    public override void Updateme()
    {
        position = transform.position;
    }
    void Start () {
        //向管理器注册这个触发器，管理器会把它加入当前触发器列表中
        manager.RegisterTrigger(this);
	}
}
