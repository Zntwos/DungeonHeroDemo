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
    /// ����ع���ϵͳ
    /// </summary>
    public class ObjectPoolSystem : AbstractSystem, IObjectPoolSystem
    {
        /// <summary>
        /// ������ֵ�����(�¹�)
        /// </summary>
        private Dictionary<string, PoolData> mPoolDic;
        /// <summary>
        /// ����ظ�����
        /// </summary>
        private Transform mPoolRoot;

        GameObject IObjectPoolSystem.Get(string name)
        {
            return mPoolDic.TryGetValue(name, out PoolData data) && data.CanGet ? data.Get() : new GameObject(name);
        }

        void IObjectPoolSystem.Get(string name, Action<GameObject> callBack)
        {
            //����ж�Ӧ�ĸ��� ��������ж��� ��ȡ����
            if (mPoolDic.TryGetValue(name, out PoolData data) && data.CanGet)
            {
                if (callBack == null) data.Get();
                else callBack(data.Get());
                return;
            }
            //�첽������Դ ����������ⲿ�� ����ص�������Ϊ�� ���׳��ö���
            ResHelper.AsyncLoad<GameObject>(name, o =>
            {
                //if(o != null)
                o.name = name;
                callBack?.Invoke(o);
            });
        }
        /// <summary>
        /// �Ѽ��ص���Դ���뻺���
        /// </summary>
        void IObjectPoolSystem.Recovery(GameObject obj)
        {
            if (mPoolDic.TryGetValue(obj.name,out var data))
            {
                data.Push(obj);
                return;
            }
            //�ж��Ƿ��и����� û�оʹ���һ��   
            if (mPoolRoot == null) mPoolRoot = new GameObject("PoolRoot").transform;
            mPoolDic.Add(obj.name, new PoolData(obj, mPoolRoot));
        }
        /// <summary>
        /// ��ջ���� �����л�
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
    /// Unity ��Ϸ���󻺴��
    /// </summary>
    public class PoolData
    {
        /// <summary>
        /// �ɼ������Ķ���
        /// </summary>
        private Queue<GameObject> mActivatableObject = new Queue<GameObject>();
        /// <summary>
        /// ���Ի�ȡ�����ʶ
        /// </summary>
        public bool CanGet => mActivatableObject.Count > 0;
        /// <summary>
        /// ������صĸ��ڵ�
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