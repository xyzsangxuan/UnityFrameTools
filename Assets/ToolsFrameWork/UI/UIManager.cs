using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// UI层级
/// </summary>
public enum E_UI_Layer
{
    Bot,
    Mid,
    Top,
    System,
    Null,
    Twice,
}

/// <summary>
/// UI管理器
/// 1.管理所有显示的面板
/// 2.提供给外部 显示和隐藏等接口
/// </summary>
public class UIMgr : Singleton<UIMgr>
{
    public GameObject UICanvas;//Canvas
    public GameObject UIEventSystem;//EventSystem
    public Camera UICamera;//Camera;
                           //-----
    public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    public Transform bot;
    public Transform mid;
    public Transform top;
    public Transform system;
    public Transform twice;
    //-----
    private string panelFulName;
    private string panelNameSpace;
    public UIMgr()
    {

    }

    //事件系统启用
    public bool UIEventSystemEnabled
    {
        set { UIEventSystem.GetComponent<EventSystem>().enabled = value; }
        get { return UIEventSystem.GetComponent<EventSystem>().enabled; }
    }


    /// <summary>
    ///  显示面板
    /// </summary>
    /// <typeparam name="T">面板脚本类型</typeparam>
    /// <param name="panelName">面板名</param>
    /// <param name="layer">显示在哪一层</param>
    /// <param name="callBack">当面板创建成功后，你想做的事</param>
    public void ShowPanel<T>(string panelName = "", E_UI_Layer layer = E_UI_Layer.Mid, UnityAction<T> callBack = null) where T : BasePanel
    {
        panelFulName = typeof(T).FullName;
        panelNameSpace = typeof(T).Namespace + ".";
        //T ui = default;
        if (panelName == "") panelName = panelFulName.Replace(panelNameSpace, "");

        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].ShowMe();
            //处理面板创建完成后的逻辑
            if (callBack != null)
                callBack(panelDic[panelName] as T);
            return;
        }

        ResMgr.GetInstance().LoadAsync<GameObject>(PathCfg.PATH_UI + panelName, (obj) => {
            //把他作为Canvas的子对象
            //并且要设置它的相对位置
            //找到父对象 你到底显示在那一层
            Transform father = bot;
            switch (layer)
            {
                case E_UI_Layer.Mid:
                    father = mid;
                    break;
                case E_UI_Layer.Top:
                    father = top;
                    break;
                case E_UI_Layer.System:
                    father = system;
                    break;
                case E_UI_Layer.Null:
                    father = system.parent;
                    break;
                case E_UI_Layer.Twice:
                    father = twice;
                    break;

                default:
                    father = system.parent;
                    break;

            }
            //设置父对象 设置相对位置和大小
            obj.transform.SetParent(father);

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;

            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            //得到预设体身上的面板脚本
            T panel = obj.GetComponent<T>();

            //处理面创建完成后的逻辑
            if (callBack != null)
                callBack(panel);

            panel.ShowMe();

            // 把面板存起来
            panelDic.Add(panelName, panel);


        });

        return;
    }

    public GameObject ShowPanel<T>(int temp, string panelName = "", E_UI_Layer layer = E_UI_Layer.Mid) where T : BasePanel
    {
        panelFulName = typeof(T).FullName;
        panelNameSpace = typeof(T).Namespace + ".";
        //T ui = default;
        if (panelName == "") panelName = panelFulName.Replace(panelNameSpace, "");

        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].ShowMe();

            return panelDic[panelName].gameObject;
        }


        GameObject obj = ResMgr.GetInstance().Load(PathCfg.PATH_UI + panelName);

        //把他作为Canvas的子对象
        //并且要设置它的相对位置
        //找到父对象 你到底显示在那一层
        Transform father = bot;
        switch (layer)
        {
            case E_UI_Layer.Mid:
                father = mid;
                break;
            case E_UI_Layer.Top:
                father = top;
                break;
            case E_UI_Layer.System:
                father = system;
                break;
            case E_UI_Layer.Null:
                father = system.parent;
                break;
            case E_UI_Layer.Twice:
                father = twice;
                break;

            default:
                father = system.parent;
                break;

        }
        //设置父对象 设置相对位置和大小
        obj.transform.SetParent(father);

        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;

        (obj.transform as RectTransform).offsetMax = Vector2.zero;
        (obj.transform as RectTransform).offsetMin = Vector2.zero;

        //得到预设体身上的面板脚本
        T panel = obj.GetComponent<T>();

        panel.ShowMe();

        // 把面板存起来
        panelDic.Add(panelName, panel);


        return panelDic[panelName].gameObject;
    }





    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <param name="panelName"></param>
    public void HidePanel(string panelName, bool Destroy = true)
    {
        Debug.Log(panelName);
        if (Destroy)
        {
            if (panelDic.ContainsKey(panelName))
            {
                panelDic[panelName].HideMe();
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }
        else
        {
            Debug.Log("暂时还没做隐藏不删除");
        }

    }
    /// <summary>
    /// 隐藏所有面板
    /// </summary>
    /// <param name="panelName"></param>
    public void HideAllPanel()
    {
        if (panelDic.Count <= 0) return;
        foreach (KeyValuePair<string, BasePanel> item in panelDic)
        {
            if (panelDic[item.Key] != null)
                GameObject.Destroy(panelDic[item.Key].gameObject);
        }
        panelDic.Clear();


    }

    //删除某一层所有的UI
    internal void RemoveLayer(E_UI_Layer layer = E_UI_Layer.Mid)
    {
        switch (layer)
        {
            case E_UI_Layer.Mid:
                for (int i = 0; i < mid.childCount; i++)
                {
                    panelDic.Remove(mid.GetChild(i).name);
                }
                mid.gameObject.DestroyAllChildren();
                break;
            case E_UI_Layer.Top:
                for (int i = 0; i < top.childCount; i++)
                {
                    panelDic.Remove(top.GetChild(i).name);
                }
                top.gameObject.DestroyAllChildren();
                break;
            case E_UI_Layer.System:
                for (int i = 0; i < system.childCount; i++)
                {
                    panelDic.Remove(system.GetChild(i).name);
                }
                system.gameObject.DestroyAllChildren();
                break;
            case E_UI_Layer.Null:

                break;
            case E_UI_Layer.Twice:
                for (int i = 0; i < twice.childCount; i++)
                {
                    panelDic.Remove(twice.GetChild(i).name);
                }
                twice.gameObject.DestroyAllChildren();
                break;
            default:
                for (int i = 0; i < system.childCount; i++)
                {
                    panelDic.Remove(system.GetChild(i).name);
                }
                system.parent.gameObject.DestroyAllChildren();
                break;

        }
    }



}
