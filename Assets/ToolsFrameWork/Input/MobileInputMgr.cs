using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 移动端输入控制模块
/// </summary>
public class MobileInputMgr : Singleton<InputMgr>
{
    public bool isStart = false;

    

    /// <summary>
    /// 构造函数 添加Update监听
    /// </summary>
    public MobileInputMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(MyUpdate);
    }

    public void StartOrEndCheck(bool isOpen)
    {
        isStart = isOpen;
    }
    private void CheckButton(Button btn)
    {
        
    }
    private void MyUpdate()
    {
        //没有开启输入检测 就不去检测 直接return
        if (!isStart)
            return;
        
    }
}
