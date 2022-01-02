using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
///  面板基类
///  找到所有自己面板下的控件对象
///  他应该提供显示 或者隐藏的行为 
///  方便我们在子类中处理逻辑
///  节约找控件的工作量
/// </summary>
public class BasePanel : MonoBehaviour
{
    //通过里氏转换原则 来存储所有的控件
    private Dictionary<string, List<UIBehaviour>> controlDIC = new Dictionary<string, List<UIBehaviour>>();
    // Start is called before the first frame update
    void Awake()
    {
        FindChildrenControl<Button>();
        FindChildrenControl<Image>();
        FindChildrenControl<Text>();
        FindChildrenControl<Slider>();
        FindChildrenControl<ScrollRect>();
        FindChildrenControl<InputField>();
        FindChildrenControl<Toggle>();
    }

    /// <summary>
    /// 显示自己
    /// </summary>
    public virtual void ShowMe()
    {

    }
    /// <summary>
    /// 隐藏自己
    /// </summary>
    public virtual void HideMe()
    {

    }

    /// <summary>
    /// 得到对应名字的对应控件脚本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controllName"></param>
    /// <returns></returns>
    protected T GetControl<T>(string controllName) where T:UIBehaviour
    {
        if (controlDIC.ContainsKey(controllName)){
            for(int i = 0;i < controlDIC[controllName].Count; ++i)
            {
                if(controlDIC[controllName][i] is T)
                    return controlDIC[controllName][i] as T;
            }
        }
        return null;
    }
   

    /// <summary>
    /// 找到子对象的对应控件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void FindChildrenControl<T>() where T:UIBehaviour
    {
        T[] controls = this.GetComponentsInChildren<T>();
        string objName;
        for (int i = 0; i < controls.Length; ++i)
        {
            objName = controls[i].gameObject.name;
            if (controlDIC.ContainsKey(objName))
                controlDIC[objName].Add(controls[i]);
            else
                controlDIC.Add(controls[i].gameObject.name, new List<UIBehaviour> { controls[i] });
        }
    }
}
