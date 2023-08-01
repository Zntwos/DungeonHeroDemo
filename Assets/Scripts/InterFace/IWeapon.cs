using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
namespace DungeonHero
{
    public interface IWeapon : IDrops
    {
        void disposed();
    }
}
