using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace DungeonHero
{
    public class BulletCtrl : MonoBehaviour, IController
    {
        [Range(0f, 30f)]
        public float speed;
        private IObjectPoolSystem _poolSystem;

        void Start()
        {
            _poolSystem = this.GetSystem<IObjectPoolSystem>();
            
        }
        private void OnEnable()
        {
            StartCoroutine(BulletRecovery());
        }
        private void OnDisable()
        {
            StopCoroutine(BulletRecovery());
        }
        // Update is called once per frame
        void Update()
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Wall")
            {
                Debug.Log("Åö×²µ½Ç½±Ú");
            }
            Destroy(gameObject);
        }
        
        IEnumerator BulletRecovery()
        {
            yield return new WaitForSeconds(1);
            _poolSystem.Recovery(gameObject);
        }
        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }

}
