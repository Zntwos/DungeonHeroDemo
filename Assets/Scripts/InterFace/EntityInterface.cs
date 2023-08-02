using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public interface IGetHit
    {
        float maxHealth { get; set; }
        float currentHealth { get; set; }
        void GetHit(float damage);
        void OnDeath();
    }
    public interface IAttack
    {

    }
}

