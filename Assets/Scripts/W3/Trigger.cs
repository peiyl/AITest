using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    //保存管理中心对象
    protected TriggerSystemManager manager;
    //触发器位置
    protected Vector3 position;
    //触发器的半径
    public int radius;
    //当前的触发器是否需要被移除
    public bool toBeRemoved;
    //检查作为参数的感知器S是否在触发器的作用范围内
    //如果是，采取相应行动，这个方法需要在派生类中实现
    public virtual void Try(Sensor s) { }
    //这个方法更新触发器的内部状态，例如，声音触发器剩余有效时间等。
    public virtual void Updateme() { }
    //检查感知器S是否在触发器作用范围内，
    //返回一个布尔值，被try（）调用，需要在派生类实现
    protected virtual bool isTouchingTrigger(Sensor sensor)
    {
        return false;
    }
    private void Awake()
    {
        //查找管理器并保存
        manager = FindObjectOfType<TriggerSystemManager>();
    }

    void Start () {
        //这时不需要被移除，设置为false；
        toBeRemoved = false;
	}
}
