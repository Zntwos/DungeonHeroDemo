using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public interface IDrops
    {
        void PickedUp(GameObject Receiver);
    }
    public interface IWeapon : IDrops
    {
        void disposed();
    }
    public interface IReward : IDrops
    {

    }
}

