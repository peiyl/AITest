using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : TriggerLimitedLifetime {
    //判断感知体能否听到声音触发器发出的声音，如果能，通知感知器
    public override void Try(Sensor s)
    {
        if (isTouchingTrigger(s))
        {
            s.Notify(this);
        }
    }
    //判断感知体是否能听到声音触发器发出的声音；
    protected override bool isTouchingTrigger(Sensor sensor)
    {
        GameObject g = sensor.gameObject;
        //如果感知器能够感知声音
        if (sensor.sensorType == Sensor.SensorType.sound)
        {
            //如果感知体与声音触发器的距离在声音触发器的作用范围内，返回ture
            if ((Vector3.Distance(transform.position,g.transform.position))<radius)
            {
                return true;
            }
        }
        return false;
    }
    void Start () {

        //设置该触发器的持续时间
        lifeTime = 3;
        manager.RegisterTrigger(this);
	}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
