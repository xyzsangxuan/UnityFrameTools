using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 状态基类
/// </summary>
public abstract class FSMState
{
    public FSMStateMachine _Machine;
    public GameObject _owner;
    public virtual void Init(GameObject owner, FSMStateMachine machine)
    {
        _owner = owner;
        _Machine = machine;
    }


    //进入某个状态，选要做的准备或者初始化工作
    public virtual void EnterState()
    {
        //Debug.Log("进入"+this.ToString());
    }
    //进入某个状态中，需要持续更新的逻辑
    public virtual void UpdateState() { }
    //退出某个状态，需要重置的一些数据
    public virtual void ExitState()
    {
        //Debug.Log("退出"+this.ToString());
    }
}
/// <summary>
/// 有限状态机
/// </summary>
public class FSMStateMachine
{
    private Dictionary<string, FSMState> statesDic = new Dictionary<string, FSMState>();//存储一个状态机所拥有的状态
    public FSMState currentFsmState;//当前状态
    private FSMState lastFsmState;//上一个状态

    //通过构造函数初始化一些数据
    public FSMStateMachine()
    {

    }
    //注册状态
    public void RegisterState(string key, FSMState value)//哪个子状态进行了注册，这个子状态的machine这个字段就指向组侧着（StateMachine的一个实例）
    {
        statesDic.Add(key, value);
    }
    //设置默认状态
    public void SetDefaultState(string key)
    {
        //找到字典的key所对应的状态
        if (statesDic.ContainsKey(key))
        {
            currentFsmState = statesDic[key];
        }
        else
        {
            Debug.LogError("没有此状态无法设置，设置默认状态失败！");
        }
    }
    //切换状态
    public void ChangeState(string key)
    {
        //找到字典的key所对应的状态
        if (statesDic.ContainsKey(key))
        {
            currentFsmState = statesDic[key];
        }
        else
        {
            Debug.LogError("没有此状态无法设置，切换状态失败！");
        }
    }
    //机器工作的方法
    public void DoWork()
    {
        if (currentFsmState != lastFsmState)
        {
            currentFsmState.EnterState();
            lastFsmState = currentFsmState;
        }

        currentFsmState.UpdateState();// 只有在UpdateState中切换状态才能触发退出
        if (currentFsmState != lastFsmState)
        {
            lastFsmState.ExitState();
        }
    }
}