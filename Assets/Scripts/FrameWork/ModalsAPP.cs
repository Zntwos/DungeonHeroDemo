using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System.Runtime.InteropServices;
using Unity.VisualScripting;

namespace DungeonHero
{
    public class ModalsApp : Architecture<ModalsApp>
    {
        protected override void Init()
        {
            this.RegisterModel<INumModal>(new NumModal());
            this.GetModel<INumModal>().GoldScore.Value = 0;
        }
    }
}

