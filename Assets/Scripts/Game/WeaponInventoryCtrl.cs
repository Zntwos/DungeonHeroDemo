using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System.ComponentModel;
using UnityEngine.PlayerLoop;
using Unity.VisualScripting;

namespace DungeonHero
{
    public class WeaponInventoryCtrl : MonoBehaviour, IController
    {
        private IWeaponInventorySystem _weaponInventorySystem;

        private IWeapon _iWeapon;
        [SerializeField]
        private int currentWeapon;
        [SerializeField]
        private List<GameObject> Weapons = new List<GameObject>(3) { null, null, null };

        private void Start()
        {
            _weaponInventorySystem = this.GetSystem<IWeaponInventorySystem>();

        }

        private void Update()
        {
            _weaponInventorySystem.CurrentWeaponSwitching(Input.GetAxis("Mouse ScrollWheel"));
            currentWeapon = _weaponInventorySystem.currentWeaponIndex;
            Weapons = _weaponInventorySystem.weaponInventory;
            //Debug.Log("µ±Ç°ÎäÆ÷Ë÷Òý:" + currentWeapon);

            //if (Input.GetKeyDown(KeyCode.Space))
            _weaponInventorySystem.AutoWeaponSwitching();

            if (!Input.GetKeyDown(KeyCode.Q) || _weaponInventorySystem.weaponInventory[currentWeapon] == null) return;
            _weaponInventorySystem.RemoveWeapon(currentWeapon);
        }
        

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDrops drop))
            {
                if (!Input.GetKeyDown(KeyCode.F)) return;
                _weaponInventorySystem.AddWeapon(collision.gameObject);
                //Debug.Log(collision.gameObject);
                drop.PickedUp(gameObject);
            }
        }


        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return GameApp.Interface;
        }
    }

}

