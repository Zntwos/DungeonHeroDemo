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
        public Image weaponSlot;
        private Text _goldNum;
        private void Start()
        {
            _goldNum = transform.Find("GoldScores").GetComponent<Text>();
            
            this.GetModel<INumModal>().GoldScore.RegisterWithInitValue(GoldScore =>
            {
                _goldNum.text = GoldScore.ToString();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            
            this.GetModel<ISpriteModal>().weaponIcon.RegisterWithInitValue(weaponIcon =>
            {
                weaponSlot.sprite = weaponIcon;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
        
        
    }
}

