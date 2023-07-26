using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace DungeonHero
{
    public class UICtrl : MonoBehaviour, IController
    {
        private Text _goldNum;
        private void Start()
        {
            _goldNum = transform.Find("GoldScores").GetComponent<Text>();
            this.GetModel<INumModal>().GoldScore.RegisterWithInitValue(GoldScore =>
            {
                _goldNum.text = GoldScore.ToString();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
        public IArchitecture GetArchitecture()
        {
            return ModalsApp.Interface;
        }
    }
}

