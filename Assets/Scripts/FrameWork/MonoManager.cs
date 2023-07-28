using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace QFramework
{
    public class MonoManager : BaseManager<MonoManager>
    {
        private MonoController _controller;
        public MonoManager()
        {
            GameObject obj = new GameObject("MonoController");
            _controller = obj.AddComponent<MonoController>();
        }
        public void AddUpdateListener(UnityAction fun)
        {
            _controller.AddUpdateListener(fun);
        }
        public void DeleteUpdateListener(UnityAction fun)
        {
            _controller.DeleteUpdateListener(fun);
        }
        public Coroutine StartCoroutine(string methodName)
        {
            return _controller.StartCoroutine(methodName);
        }
        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
        {
            return _controller.StartCoroutine(methodName, value);
        }
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return _controller.StartCoroutine(routine);
        }
        public Coroutine StartCoroutine_Auto(IEnumerator routine)
        {
            return _controller.StartCoroutine(routine);
        }
        public void StopCoroutine(IEnumerator routine)
        {
            _controller.StopCoroutine(routine);
        }
        public void StopCoroutine(Coroutine routine)
        {
            _controller.StopCoroutine(routine);
        }
    }

}
