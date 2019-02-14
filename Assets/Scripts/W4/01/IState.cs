using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 实现状态机的接口
/// </summary>
public interface IState{
    /// <summary>
    /// 进入状态
    /// </summary>
    /// <param name="prevState"></param>
    void OnEnter(string prevState);
    /// <summary>
    /// 停止状态
    /// </summary>
    /// <param name="nextState"></param>
    void OnExit(string nextState);
    /// <summary>
    /// 更新状态
    /// </summary>
    void OnUpdate();
}
