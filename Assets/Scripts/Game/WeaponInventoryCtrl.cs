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
        private ITimerSystem _timerSystem;

        public int currentWeapon;

        private bool _canPick = true;

        private void Start()
        {
            _weaponInventorySystem = this.GetSystem<IWeaponInventorySystem>();
            _timerSystem = this.GetSystem<ITimerSystem>();
        }

        private void Update()
        {
            _weaponInventorySystem.CurrentWeaponSwitching(Input.GetAxis("Mouse ScrollWheel"));
            currentWeapon = _weaponInventorySystem.currentWeaponIndex;
            _weaponInventorySystem.AutoWeaponSwitching();

            if (!Input.GetKeyDown(KeyCode.Q) || _weaponInventorySystem.weaponInventory[currentWeapon] == null) return;
            _weaponInventorySystem.RemoveWeapon(currentWeapon);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!Input.GetKey(KeyCode.F) || !_canPick || !(_weaponInventorySystem.Count() < 3)) return;
            if (collision.TryGetComponent(out IWeapon drop))
            {
                _weaponInventorySystem.AddWeapon(collision.gameObject);
                drop.PickedUp(gameObject);
                _canPick = false;
                _timerSystem.AddTimer(PickEventCooldown, 0.2f, false);
            }
        }

        private void PickEventCooldown()
        {
            _canPick = true;
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return GameApp.Interface;
        }
    }

}

