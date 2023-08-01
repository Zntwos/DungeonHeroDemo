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
        private IWeapon _iWeapon;
        [SerializeField]
        private List<Sprite> _weaponIcons = new List<Sprite>(3) { null, null, null };
        [SerializeField]
        private List<Sprite> _availableWeaponIcon = new List<Sprite>();
        [SerializeField]
        int index = 0;
        private void Start()
        {
            _iWeapon=GetComponent<IWeapon>();
            _weaponInventorySystem = this.GetSystem<IWeaponInventorySystem>();
            _goldNum = transform.Find("GoldScores").GetComponent<Text>();
            
            this.GetModel<INumModal>().GoldScore.RegisterWithInitValue(GoldScore =>
            {
                _goldNum.text = GoldScore.ToString();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            
            //this.GetModel<ISpriteModal>().weaponIcon.RegisterWithInitValue(weaponIcon =>
            //{
            //    weaponSlot.sprite = weaponIcon;
            //}).UnRegisterWhenGameObjectDestroyed(gameObject);

            //this.RegisterEvent<WeaponInventoryChangeEvent>(e=> { GetAvailableWeaponIcon(); }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
        private void Update()
        {
            ////weaponSlot.sprite = _availableWeaponIcon[_weaponInventorySystem.currentWeaponIndex];
            //if(index != _weaponInventorySystem.currentWeaponIndex)
            //{
            //    index = _weaponInventorySystem.currentWeaponIndex;
            //    weaponSlot.sprite = _weaponIcons[_weaponInventorySystem.currentWeaponIndex];
            //}
        }
        //private void GetAvailableWeaponIcon()
        //{
        //    Debug.Log("ÎäÆ÷Í¼±ê¸üÐÂ");
        //    _weaponIcons.Clear();
        //    var weapons = this.GetModel<IWeaponModal>().weaponInventory.Value;
        //    for (int i = 0; i < weapons.Count; i++)
        //    {
        //        if (weapons[i] == null) continue;
        //        _weaponIcons[i] = 
        //    }
        //}
        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
        
        
    }
}

