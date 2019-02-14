using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseMemory : MonoBehaviour {
    //已经在列表中？
    private bool alreadyInList = false;
    //记忆留存时间
    public float memoryTime = 4.0f;
    //记忆列表
    public List<MemoryItem> memoryList = new List<MemoryItem>();
    //此时需要从记忆列表中删除的项
    private List<MemoryItem> removeList = new List<MemoryItem>();
    //在记忆列表中寻找玩家信息
    public bool FindInList()
    {
        foreach (MemoryItem item in memoryList)
            if (item.g.tag == "Player")
                return true;
        return false;
    }
    /// <summary>
    /// 向记忆列表中添加一个项
    /// </summary>
    /// <param name="g">感知到的物体</param>
    /// <param name="type">通过哪种方式感知到该游戏对象，视觉为1，听觉为0.66</param>
    public void AddToList(GameObject g,float type)
    {
        alreadyInList = false;
        //如果该项已经在列表中，那么更新最后感知时间
        foreach (MemoryItem item in memoryList)
        {
            if (g == item.g)
            {
                alreadyInList = true;
                item.lastMemoryTime = Time.time;
                item.memoryTimeLeft = memoryTime;
                if (type > item.sensorType)
                    item.sensorType = type;
                break;
            }
        }
        //如果不在列表中，新建项并加入列表
        if (!alreadyInList)
            memoryList.Add(new MemoryItem(g, Time.time, memoryTime, type));
    }
	void Update () {
        removeList.Clear();
        //遍历所有项找到那些超时要忘记的，删除
        foreach (MemoryItem item in memoryList)
        {
            item.memoryTimeLeft -= Time.deltaTime;
            if (item.memoryTimeLeft <0)
            {
                removeList.Add(item);
            }
            else
            {
                //对没被删除的项，画出一条线，表示仍在记忆中
                if (item.g != null)
                    Debug.DrawLine(transform.position, item.g.transform.position, Color.blue);
            }
        }
        foreach (MemoryItem item in removeList)
        {
            memoryList.Remove(item);
        }
	}
}
public class MemoryItem
{
    //感知到的游戏对象
    public GameObject g;
    //最近的感知时间
    public float lastMemoryTime;
    //还能留存记忆的时间
    public float memoryTimeLeft;
    //通过哪种方式感知到该游戏对象，视觉为1，听觉为0.66.
    public float sensorType;
    public MemoryItem(GameObject objectToAdd,float time,float timeLeft,float type)
    {
        this.g = objectToAdd;
        this.lastMemoryTime = time;
        this.memoryTimeLeft = timeLeft;
        this.sensorType = type;
    }
}
