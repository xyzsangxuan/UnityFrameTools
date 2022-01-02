using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 输入控制模块
/// 需要具备：
/// 1.Input类
/// 2.事件中心模块
/// 3.公共mono模块
/// </summary>
public class InputMgr : Singleton<InputMgr>
{
    private bool isStart = false;

    /// <summary>
    /// 构造函数 添加Update监听
    /// </summary>
    public InputMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(MyUpdate);
    }

    public void StartOrEndCheck(bool isOpen)
    {
        isStart = isOpen;
    }

    /// <summary>
    /// 检测按键抬起按下 分发事件
    /// </summary>
    /// <param name="key"></param>
    private void CheckKeyCode(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            //事件中心模块 分发按下抬起事件
            EventCenter.GetInstance().EventTrigger<KeyCode>(EventCfg.PRESS_A_KEY, key);
        }
        if (Input.GetKeyUp(key))
        {
            //事件中心模块 分发按下抬起事件
            EventCenter.GetInstance().EventTrigger<KeyCode>(EventCfg.LIFT_A_KEY, key);
        }
    }

    private void MyUpdate()
    {
        //没有开启输入检测 就不去检测 直接return
        if (!isStart)
            return;
        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.S);
        CheckKeyCode(KeyCode.A);
        CheckKeyCode(KeyCode.D);
        CheckKeyCode(KeyCode.C);
        CheckKeyCode(KeyCode.X);
        CheckKeyCode(KeyCode.Z);
        CheckKeyCode(KeyCode.Q);
        CheckKeyCode(KeyCode.E);
        CheckKeyCode(KeyCode.Space);
    }
}
