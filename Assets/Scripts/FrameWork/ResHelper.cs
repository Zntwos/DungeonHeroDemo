using System;
using System.Collections;
using UnityEngine;

namespace QFramework
{
    /// <summary>
    /// 资源加载帮助类
    /// </summary>
    public static class ResHelper
    {
        /// <summary>
        /// 同步加载资源
        /// </summary>
        public static T SyncLoad<T>(string name) where T : UnityEngine.Object
        {
            T res = Resources.Load<T>(name);
            return res is GameObject ? GameObject.Instantiate(res) : res;
        }
        /// <summary>
        /// 异步加载资源
        /// </summary>
        public static void AsyncLoad<T>(string name, Action<T> callback) where T : UnityEngine.Object =>
            PublicMono.Instance.StartCoroutine(AsyncLoadRes(name, callback));
        /// <summary>
        /// 异步加载协程函数
        /// </summary>
        private static IEnumerator AsyncLoadRes<T>(string name, Action<T> callback) where T : UnityEngine.Object
        {
            var r = Resources.LoadAsync<T>(name);
            while (!r.isDone) yield return null;
            callback(r.asset is GameObject ? GameObject.Instantiate(r.asset) as T : r.asset as T);
        }
        /// <summary>
        /// 同步加载资源集合
        /// </summary>
        public static T[] SyncLoadAll<T>(string name) where T : UnityEngine.Object => Resources.LoadAll<T>(name);
    }
}