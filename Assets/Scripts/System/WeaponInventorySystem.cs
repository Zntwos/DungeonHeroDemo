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
        /// �Զ������л�
        /// ����ǵ�ǰ�����ı䣬
        /// �Ǿ���������������
        /// ����ǰ�����л�Ϊ�б��д������ﵱǰ���������
        /// </summary>
        public void AutoWeaponSwitching()
        {
            if (weaponInventory[currentWeaponIndex] == null)
            {
                int j = currentWeaponIndex;
                for (int i = 0; i < _weaponNum;)
                {
                    //�ж������Ƿ��ڷ�Χ��
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
        /// �����ֹ����л��б�������������ֵ
        /// </summary>
        /// <param name="axis">�����ַ���ֵ</param>
        public void CurrentWeaponSwitching(float axis)
        {
            //Debug.Log("�����б���::" + weaponInventory.Count);
            if (weaponInventory == null) return;
            //�����Ϲ�
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
            //�����¹�
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
