using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 状态切换事件
/// </summary>
public class FSEvent : MonoBehaviour {
    protected FiniteStateMachine.EnterState mEnterDelegate;
    protected FiniteStateMachine.PushState mPushDelegate;
    protected FiniteStateMachine.PopState mPopDelegate;

    protected enum EventType { NONE,ENTER,PUSH,POP};
    protected string mEventName;
    protected FSState mStateOwner;
    protected string mTargetState;
    protected FiniteStateMachine mOwner;
    protected EventType eType;
    //不知道这个是干嘛的
    public Func<object, object, object, bool> mAction = null;
    public FSEvent(string name,string target,FSState state,FiniteStateMachine owner,FiniteStateMachine.EnterState e,FiniteStateMachine.PushState pu,FiniteStateMachine.PopState po)
    {
        mStateOwner = state;
        mEventName = name;
        mTargetState = target;
        mOwner = owner;
        eType = EventType.NONE;
        mEnterDelegate = e;
        mPushDelegate = pu;
        mPopDelegate = po;
    }
    public FSState Enter(string stateName)
    {
        mTargetState = stateName;
        eType = EventType.ENTER;
        return mStateOwner;
    }
    public FSState Push(string stateName)
    {
        mTargetState = stateName;
        eType = EventType.PUSH;
        return mStateOwner;
    }
    public void Pop()
    {
        eType = EventType.POP;
    }
    /// <summary>
    /// 栈的状态切换
    /// </summary>
    /// <param name="oOne"></param>
    /// <param name="oTwo"></param>
    /// <param name="oThree"></param>
    public void Execute(object oOne,object oTwo,object oThree)
    {
        if (eType == EventType.POP)
        {
            mPopDelegate();
        }
        else if(eType == EventType.PUSH)
        {
            mPushDelegate(mTargetState, mOwner.CurrentState.StateName);
        }
        else if(eType == EventType.ENTER)
        {
            mEnterDelegate(mTargetState);
        }
        else if(mAction != null)
        {
            mAction(oOne, oTwo, oThree);
        }
    }
}
