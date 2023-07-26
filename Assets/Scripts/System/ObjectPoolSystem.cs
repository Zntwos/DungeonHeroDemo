using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System;

namespace DungeonHero
{
    public interface IObjectPoolSystem : ISystem
    {
        GameObject Get(string name);
        void Get(string name, Action<GameObject> callBack = null);
        void Recovery(GameObject obj);
        void Dispose();
    }
    public class ObjectPoolSystem : AbstractSystem, IObjectPoolSystem
    {
        private Dictionary<string, PoolData> _poolDict;
        private Transform _poolRoot;

        protected override void OnInit()
        {
            _poolDict = new Dictionary<string, PoolData>();
        }

        void IObjectPoolSystem.Dispose()
        {
            _poolDict.Clear();
            _poolRoot = null;
        }

        GameObject IObjectPoolSystem.Get(string name)
        {
            return _poolDict.TryGetValue(name, out PoolData poolData) && poolData.canGet ? poolData.Get() : new GameObject();
        }

        void IObjectPoolSystem.Get(string name, Action<GameObject> callBack)
        {
            if(_poolDict.TryGetValue(name, out PoolData poolData) && poolData.canGet)
            {
                if (callBack == null) poolData.Get();
                else callBack(poolData.Get());
                return;
            }
            ResHelper.AsyncLoad<GameObject>(name, r =>
            {
                r.name = name;
                callBack?.Invoke(r);
            });
        }

        void IObjectPoolSystem.Recovery(GameObject obj)
        {
            if(_poolDict.TryGetValue(obj.name,out var poolData))
            {
                _poolDict[obj.name].Push(obj);
                return;
            }
            if (_poolRoot == null) _poolRoot = new GameObject("PoolRoot").transform;
            _poolDict.Add(obj.name, new PoolData(obj, _poolRoot));
        }
    }

    public class PoolData
    {
        public Queue<GameObject> activeableObj = new Queue<GameObject>();
        public bool canGet => activeableObj.Count > 0;
        private Transform _parentRoot;

        public PoolData(GameObject obj,Transform root)
        {
            _parentRoot = new GameObject(obj.name).transform;
            _parentRoot.SetParent(root);
            Push(obj);
        }
        public GameObject Get()
        {
            GameObject obj = activeableObj.Dequeue();
            obj.SetActive(true);
            obj.transform.SetParent(null);
            return obj;
        }
        public void Push(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(_parentRoot);
            activeableObj.Enqueue(obj);
        }
    }
}