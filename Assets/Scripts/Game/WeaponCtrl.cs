using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace DungeonHero
{
    public class WeaponCtrl : MonoBehaviour, IController
    {
        

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        
        public IArchitecture GetArchitecture()
        {
            return ModalsApp.Interface;
        }
    }
}

