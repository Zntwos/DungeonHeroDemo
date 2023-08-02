using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System;
using UnityEngine.Events;

namespace DungeonHero
{
    public class RewardCtrl : MonoBehaviour, IController, IReward
    {
        private IObjectPoolSystem _poolSystem;
        UnityAction action;

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
        private void Start()
        {
            _poolSystem = this.GetSystem<IObjectPoolSystem>();

        }
        private void OnDisable()
        {
            MonoManager.GetInstance().DeleteUpdateListener(action);
        }
        public void PickedUp(GameObject receiver)
        {
            action = () =>
            {
                transform.position = Vector2.Lerp(transform.position, receiver.transform.position, Time.deltaTime * 20);
                if (Vector2.Distance(transform.position, receiver.transform.position) < 0.1f)
                {
                    this.GetModel<INumModal>().GoldScore.Value++;
                    _poolSystem.Recovery(gameObject);
                }
            };
            MonoManager.GetInstance().AddUpdateListener(action);
        }

        void Reward(GameObject receiver)
        {
            
        }
    }
}

