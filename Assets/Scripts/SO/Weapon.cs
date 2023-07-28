using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace DungeonHero
{
    [CreateAssetMenu(fileName = "Weapon",menuName = "Customization/weapon")]
    public class Weapon : ScriptableObject
    {
        public string weaponName;
        public enum weaponTypes { Gun, Sword}
        public weaponTypes weaponType;
        public string weaponId;
        public float attackInterval;
        public Sprite icon;
        public GameObject weapon;
    }
}

