using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHealthGiver : TriggerRespawning {
    //设置每次增加的生命值
    public int healthGiven = 10;
    //检测当前触发器是否是活动的，并且感知器是否在这个触发器的作用范围内
    public override void Try(Sensor s)
    {
        if (isActive&&isTouchingTrigger(s))
        {
            AIController controller = s.GetComponent<AIController>();
            if (controller != null)
            {
                //增加生命值
                controller.health += healthGiven;
                //显示当前生命值
                print("now my health is :"+controller.health);
                //将它的颜色变为绿色
                GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                //调用coroutine开始计时
                //调用感知器的Notify函数，以便感知体做出相应行动；
                StartCoroutine("TurnColorBack");
                s.Notify(this);
            }
            else
                print("Can't get health script!");
            Deactivate();
        }
    }
    //3秒后生命值供给器变成黑色，表示处于非激活状态
    IEnumerator TurnColorBack()
    {
        yield return new WaitForSeconds(3);
        GetComponent<Renderer>().material.SetColor("_Color", Color.black);
    }
    //检查感知器是否在这个触发器的作用范围内
    protected override bool isTouchingTrigger(Sensor sensor)
    {
        GameObject g = sensor.gameObject;
        //如果感觉得到
        if (sensor.sensorType == Sensor.SensorType.health)
        {
            //触发器与感知器的距离是否小于触发器的作用半径
            if ((Vector3.Distance(transform.position,g.transform.position))<radius)
            {
                return true;
            }
        }
        return false;
    }
    void Start () {
        //设置两次活动状态之间的间隔时间；
        numUpdatesBetweenRespawns = 6000;
        //向管理器注册这个触发器
        manager.RegisterTrigger(this);
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
