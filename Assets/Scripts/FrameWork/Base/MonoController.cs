using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace QFramework
{
    public class MonoController : MonoBehaviour
    {

        private event UnityAction _updateEvent;

        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        void Update()
        {
            if (_updateEvent != null)
                _updateEvent.Invoke();
        }
        /// <summary>
        /// ���ⲿ�ṩ�����֡�����¼�
        /// </summary>
        /// <param name="fun"></param>
        public void AddUpdateListener(UnityAction fun)
        {
            _updateEvent += fun;
        }
        /// <summary>
        /// ���ⲿ�ṩ���Ƴ�֡�����¼�
        /// </summary>
        /// <param name="fun"></param>
        public void DeleteUpdateListener(UnityAction fun)
        {
            _updateEvent -= fun;
        }
    }

}
