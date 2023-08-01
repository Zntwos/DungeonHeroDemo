using DungeonHero;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonHero
{
    public class GameApp : Architecture<GameApp>
    {
        protected override void Init()
        {
            this.RegisterModel<INumModal>(new Modal());
            this.RegisterModel<ISpriteModal>(new Modal());
            this.RegisterModel<IWeaponModal>(new Modal());
            this.RegisterSystem<IObjectPoolSystem>(new ObjectPoolSystem());
            this.RegisterSystem<IWeaponInventorySystem>(new WeaponInventorySystem());
        }
    }
}
