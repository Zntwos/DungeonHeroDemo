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
        private IWeaponInventorySystem _weaponInventorySystem;
        private void Start()
        {
            _weaponInventorySystem = this.GetSystem<IWeaponInventorySystem>();
            _goldNum = transform.Find("GoldScores").GetComponent<Text>();
            
            this.GetModel<INumModal>().GoldScore.RegisterWithInitValue(GoldScore =>
            {
                _goldNum.text = GoldScore.ToString();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            this.RegisterEvent<WeaponSwitchEvent>(e =>
            {
                var w = _weaponInventorySystem.weaponInventory[_weaponInventorySystem.currentWeaponIndex];
                if (w == null)
                {
                    weaponSlot.sprite = null;
                    return;
                }
                else
                {
                    var sp = w.GetComponent<WeaponCtrl>().weapon.icon;
                    weaponSlot.sprite = sp != null ? sp : null;
                }
            });
        }
        private void Update()
        {
            if (weaponSlot.sprite != null)
            {
                weaponSlot.color = new Color(225, 225, 225, 225);
                return;
            }
            weaponSlot.color = new Color(225, 225, 225, 0);
        }
        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
        
        
    }
}

