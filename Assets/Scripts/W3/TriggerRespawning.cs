using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRespawning : Trigger {
    //两次活跃之间的间隔时间
    protected int numUpdatesBetweenRespawns;
    //距离下次再生还需要等待的时间
    protected int numUpdatesRemainingUntilRespawn;
    //当前是否是活动状态
    protected bool isActive;
    //设置isActive为活动状态
    protected void SetActive()
    {
        isActive = true;
    }
    //设置为非活动状态
    protected void SetInactive()
    {
        isActive = false;
    }
    /// <summary>
    /// 将触发器设置为非活动状态
    /// </summary>
    protected void Deactivate()
    {
        SetInactive();
        numUpdatesRemainingUntilRespawn = numUpdatesBetweenRespawns;
    }
    public override void Updateme()
    {
        //倒计时
        if ((--numUpdatesRemainingUntilRespawn <= 0) && !isActive)
            SetActive();
    }
    protected void Start () {
        //当前是活动的
        isActive = true;
	}
}
