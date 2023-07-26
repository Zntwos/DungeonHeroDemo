using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System;

namespace DungeonHero
{
    public interface IEquipSystem : ISystem
    {
        GameObject GetEquipment(string name);
        void GetEquipment(string name, Action<GameObject> callBack = null);
        void HandheldEquipment(int index);
        void DisposeEquip(int index);   
    }
    public class EquipSystem : AbstractSystem, IEquipSystem
    {
        private GameObject currentHandheldEquip = null;
        private int currentEquipIndex = -1;
        private List<GameObject> equipList = new List<GameObject>();
        protected override void OnInit()
        {
            
        }

        void IEquipSystem.DisposeEquip(int index)
        {
            
        }

        GameObject IEquipSystem.GetEquipment(string name)
        {
            throw new NotImplementedException();
        }

        void IEquipSystem.GetEquipment(string name, Action<GameObject> callBack)
        {
            throw new NotImplementedException();
        }

        void IEquipSystem.HandheldEquipment(int index)
        {
            throw new NotImplementedException();
        }
    }
}

