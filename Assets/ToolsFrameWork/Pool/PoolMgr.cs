using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _0._1FrameWork.PoolMgr
{
    /// <summary>
    /// 抽屉数据 池子中的一列容器
    /// </summary>
    public class PoolData
    {
        //抽屉中 对象挂载的父节点
        public GameObject fatherObj;
        //对象的容器
        public List<GameObject> poolList;
        /// <summary>
        /// 构造函数用来初始化根对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="poolObj"></param>
        public PoolData(GameObject obj, GameObject poolObj)
        {
            //给抽屉创建一个父对象，并且把它作为pool对象的子物体
            fatherObj = new GameObject(obj.name);
            fatherObj.transform.parent = poolObj.transform;
            fatherObj.transform.localPosition = Vector3.zero;
            poolList = new List<GameObject>() { };
            PushObj(obj);
        }

        /// <summary>
        /// 往抽屉里面压东西
        /// </summary>
        /// <param name="obj"></param>
        public void PushObj(GameObject obj)
        {
            //失活，使其隐秘
            obj.SetActive(false);
            //存起来
            poolList.Add(obj);

            try
            {
                //设置父对象
                obj.transform.parent = fatherObj.transform;
                //
                obj.transform.localPosition = Vector3.zero;
            }
            catch (System.Exception)
            {
            }
        }

        /// <summary>
        ///  从抽屉里面取东西
        /// </summary>
        /// <returns></returns>
        public GameObject GetObj()
        {
            GameObject obj = null;
            //去除第一个
            obj = poolList[0];
            //取出去
            poolList.RemoveAt(0);
            //激活，使其显示
            obj.SetActive(true);
            //断开父节点
            obj.transform.parent = null;

            return obj;
        }

    }

    /// <summary>
    /// 缓存池管理器模块
    /// 需要具备：
    /// 1.Dictionary List
    /// 2.GameObject 和Resources两个公共类中的API
    /// </summary>
    public class PoolMgr : Singleton<PoolMgr>
    {
        //缓存池容器（衣柜）
        public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();

        private GameObject poolObj;

        /// <summary>
        /// 从缓存池往外拿东西
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public void GetObj(string name, UnityAction<GameObject> callback)//把预设体的路径作为name传入
        {
            //有抽屉，并且抽屉里面有东西
            if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
            {
                callback(poolDic[name].GetObj());
            }
            else
            {
                //通过异步加载资源 创建对象给外部用
                ResMgr.GetInstance().LoadAsync<GameObject>(name, (o) => { o.name = name; callback(o); });
                //obj = GameObject.Instantiate(Resources.Load<GameObject>(name));//实例化一个对象
                //把对象名字改的和缓存池一致
                //obj.name = name;
            }
        }

        /// <summary>
        /// 还暂时不用的实例化物体给缓存池
        /// </summary>
        public void PushObj(string name, GameObject obj)
        {
            if (poolObj == null)
            {
                poolObj = new GameObject("Pool");
                poolObj.transform.position = Vector3.one * 9999f;
            }


            //里面有抽屉
            if (poolDic.ContainsKey(name))
            {
                poolDic[name].PushObj(obj);
            }
            else//里面没有抽屉
            {
                poolDic.Add(name, new PoolData(obj, poolObj));
            }
        }

        public void Clear()
        {
            poolDic.Clear();
            poolObj = null;
        }

    }
}