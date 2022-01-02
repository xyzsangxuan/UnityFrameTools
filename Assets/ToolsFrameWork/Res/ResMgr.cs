using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 资源加载模块
/// 1.异步加载
/// 2.委托和lambda表达式
/// 3.协程
/// 4.泛型
/// </summary>
public class ResMgr : Singleton<ResMgr>
{
    //同步加载资源
    public T Load<T>(string name) where T:Object
    {
        T res = Resources.Load<T>(name);
        //如果对象是一个GameObject类型 把它实例化后 再返回出去 外部直接使用即可
        if (res is GameObject)
            return GameObject.Instantiate(res);
        else//TestAssset AudioClip
            return res;
    }
    
    //异步加载资源
    public void LoadAsync<T>(string name,UnityAction<T> callback) where T:Object
    {
        //开启异步加载的协程
        MonoMgr.GetInstance().StartCoroutine(ReallyLoadAsync(name, callback));
    }

    //真正的协同程序函数，用于开启异步加载对应的资源
    private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback) where T:Object
    {
        ResourceRequest r = Resources.LoadAsync<T>(name);
        yield return r;

        if (r.asset is GameObject)
            callback(GameObject.Instantiate(r.asset) as T);
        else
            callback(r.asset as T);
    }

}
