using DungeonHero;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public interface IWeaponInventorySystem : ISystem
    {
        int currentWeaponIndex { get; set; }
        List<GameObject> weaponInventory { get; set; }
        void AddWeapon(GameObject weapon);
        void RemoveWeapon(int index);
        void CurrentWeaponSwitching(float axis);
        void AutoWeaponSwitching();
        void DisposeInventory();
        int Count();
    }
    public class WeaponInventorySystem : AbstractSystem, IWeaponInventorySystem
    {
        private int _weaponNum = 3;
        private int _changedWeaponIndex;

        private int _currentWeaponIndex;
        public int currentWeaponIndex
        {
            get { return _currentWeaponIndex; }
            set 
            {
                if (_currentWeaponIndex != value)
                {
                    _currentWeaponIndex = value;
                    this.SendEvent<WeaponSwitchEvent>();
                }

                //if (value < 0 || value > _weaponNum - 1) value = 0;
                
            }
        }
        private List<GameObject> _weaponInventory;
        public List<GameObject> weaponInventory
        {
            get { return _weaponInventory; }
            set { _weaponInventory = value; }
        }
        private List<GameObject> wI = new List<GameObject>(3) { null, null, null };
        protected override void OnInit()
        {
            _weaponInventory = new List<GameObject>(3) { null, null, null };
            weaponInventory = new List<GameObject>(3) { null, null, null };

            MonoManager.GetInstance().AddUpdateListener(DynamicListening);
        }
        private void DynamicListening()
        {
            for(int i = 0; i < 3; i++)
            {
                if (this.GetModel<IWeaponModal>().weaponInventory.Value[i] != weaponInventory[i])
                {
                    this.GetModel<IWeaponModal>().weaponInventory.Value[i] = weaponInventory[i];
                    this.SendEvent<WeaponSwitchEvent>();
                }
            }
        }

        /// <summary>
        /// 自动武器切换
        /// 如果是当前武器改变，
        /// 那就是武器被丢出，
        /// 将当前武器切换为列表中存在且里当前最近的武器
        /// </summary>
        public void AutoWeaponSwitching()
        {
            if (weaponInventory[currentWeaponIndex] == null)
            {
                int j = currentWeaponIndex;
                for (int i = 0; i < _weaponNum;)
                {
                    //判断索引是否在范围内
                    if (j > _weaponNum - 1 || j < 0)
                    {
                        j -= (int)Mathf.Pow(-1 * (i + 1), i);
                        i++;
                        continue;
                    }
                    //Debug.Log(j);
                    if (weaponInventory[j] != null)
                    {
                        currentWeaponIndex = j;
                        return;
                    }

                    j -= (int)Mathf.Pow(-1 * (i + 1), i);
                    i++;

                }
                currentWeaponIndex = 0;
            }

            for(int i = 0; i < _weaponNum; i++)
            {
                if (weaponInventory[currentWeaponIndex] == null || weaponInventory[i] == null) continue;
                if (i != currentWeaponIndex) weaponInventory[i].SetActive(false);
                else weaponInventory[i].SetActive(true);
            }
        }
        /// <summary>
        /// 鼠标滚轮滚动切换列表武器，跳过空值
        /// </summary>
        /// <param name="axis">鼠标滚轮方向值</param>
        public void CurrentWeaponSwitching(float axis)
        {
            //Debug.Log("武器列表长度::" + weaponInventory.Count);
            if (weaponInventory == null) return;
            //滚轮上滚
            if (axis > 0)
            {
                
                int j = currentWeaponIndex - 1;
                for (int i = 0; i < _weaponNum; i++)
                {
                    if (j < 0) j = _weaponNum - 1;
                    if (weaponInventory[j] != null)
                    {
                        currentWeaponIndex = j;
                        return;
                    }
                    j--;
                }
            }
            //滚轮下滚
            if (axis < 0)
            {
                int j = currentWeaponIndex + 1;
                for (int i = 0; i < _weaponNum; i++)
                {
                    if (j > _weaponNum - 1) j = 0;
                    if (weaponInventory[j] != null)
                    {
                        currentWeaponIndex = j;
                        return;
                    }
                    j++;
                }
            }
        }

        
        public void AddWeapon(GameObject weapon)
        {
            for(int i = 0; i < 3; i++)
            {
                if (weaponInventory[i] != null) continue;
                weaponInventory[i] = weapon;
                currentWeaponIndex = i;
                return;
            }
            RemoveWeapon(currentWeaponIndex);
        }

        public void RemoveWeapon(int index)
        {
            GameObject obj = weaponInventory[index];
            weaponInventory[index] = null;
            if(obj.TryGetComponent(out IWeapon weapon))
            {
                weapon.disposed();
            }
        }
        public void DisposeInventory()
        {
            currentWeaponIndex = 0;
            weaponInventory.Clear();
        }

        public int Count()
        {
            int count = 0;
            for(int i = 0; i < _weaponNum; i++)
            {
                if (weaponInventory[i] != null) count++;
            }
            return count;
        }
    }

}
