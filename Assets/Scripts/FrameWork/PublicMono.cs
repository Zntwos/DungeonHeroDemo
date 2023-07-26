using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace QFramework
{
    public class PublicMono : MonoBehaviour
    {
        public static PublicMono Instance;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public event Action OnUpdate;
        public event Action OnFixUpdate;
        public event Action OnLateUpdate;

        private void Update()
        {
            OnUpdate?.Invoke();
        }
        private void FixedUpdate()
        {
            OnFixUpdate?.Invoke();
        }
        private void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }
    }

}

