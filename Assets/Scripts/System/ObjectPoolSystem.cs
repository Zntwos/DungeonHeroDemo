using System;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public interface IObjectPoolSystem : ISystem
    {
        GameObject Get(string name);
        void Get(string name, Action<GameObject> callBack = null);
        void Recovery(GameObject obj);
        void Dispose();
    }
    /// <summary>
    /// 对象池管理系统
    /// </summary>
    public class ObjectPoolSystem : AbstractSystem, IObjectPoolSystem
    {
        /// <summary>
        /// 缓存池字典容器(衣柜)
        /// </summary>
        private Dictionary<string, PoolData> mPoolDic;
        /// <summary>
        /// 缓存池根对象
        /// </summary>
        private Transform mPoolRoot;

        GameObject IObjectPoolSystem.Get(string name)
        {
            return mPoolDic.TryGetValue(name, out PoolData data) && data.CanGet ? data.Get() : new GameObject(name);
        }

        void IObjectPoolSystem.Get(string name, Action<GameObject> callBack)
        {
            //如果有对应的格子 如果格子有东西 就取出来
            if (mPoolDic.TryGetValue(name, out PoolData data) && data.CanGet)
            {
                if (callBack == null) data.Get();
                else callBack(data.Get());
                return;
            }
            //异步加载资源 创建对象给外部用 如果回调函数不为空 则抛出该对象
            ResHelper.AsyncLoad<GameObject>(name, o =>
            {
                //if(o != null)
                o.name = name;
                callBack?.Invoke(o);
            });
        }
        /// <summary>
        /// 把加载的资源放入缓存池
        /// </summary>
        void IObjectPoolSystem.Recovery(GameObject obj)
        {
            if (mPoolDic.TryGetValue(obj.name,out var data))
            {
                data.Push(obj);
                return;
            }
            //判断是否有根对象 没有就创建一个   
            if (mPoolRoot == null) mPoolRoot = new GameObject("PoolRoot").transform;
            mPoolDic.Add(obj.name, new PoolData(obj, mPoolRoot));
        }
        /// <summary>
        /// 清空缓存池 场景切换
        /// </summary>
        void IObjectPoolSystem.Dispose()
        {
            mPoolDic.Clear();
            mPoolRoot = null;
        }

        protected override void OnInit()
        {
            mPoolDic = new Dictionary<string, PoolData>();
        }
    }
    /// <summary>
    /// Unity 游戏对象缓存池
    /// </summary>
    public class PoolData
    {
        /// <summary>
        /// 可激活对象的队列
        /// </summary>
        private Queue<GameObject> mActivatableObject = new Queue<GameObject>();
        /// <summary>
        /// 可以获取对象标识
        /// </summary>
        public bool CanGet => mActivatableObject.Count > 0;
        /// <summary>
        /// 对象挂载的父节点
        /// </summary>
        private Transform mFatherObj;

        public PoolData(GameObject obj, Transform root)
        {
            mFatherObj = new GameObject(obj.name).transform;
            mFatherObj.SetParent(root.transform);
            Push(obj);
        }
        public GameObject Get()
        {
            GameObject obj = mActivatableObject.Dequeue();
            obj.SetActive(true);
            obj.transform.SetParent(null);
            return obj;
        }
        public void Push(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(mFatherObj.transform);
            mActivatableObject.Enqueue(obj);
        }
    }
}