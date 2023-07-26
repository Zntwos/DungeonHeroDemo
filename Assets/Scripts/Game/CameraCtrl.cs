using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace DungeonHero
{
    public class CameraCtrl : MonoBehaviour
    {
        public Transform player;
        // Start is called before the first frame update
        void Start()
        {
            player = transform.Find("Player");
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(player.transform.position);
        }
    }
}


