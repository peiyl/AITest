using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLimitedLifetime : Trigger {
    //触发器持续时间
    protected int lifeTime;
    public override void Updateme()
    {
        //持续时间倒计数，如果剩余持续时间小于等于0，那么标记为需要移除
        if (--lifeTime <= 0)
        {
            toBeRemoved = true;
        }
    }
}
