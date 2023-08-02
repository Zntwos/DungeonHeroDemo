using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.UI;
namespace DungeonHero
{
    public class EnemyCtrl : MonoBehaviour, IController,IGetHit
    {
        private float _moveSpeed;

        public Image healthBar;
        public Image backPlane;
        public float maxHealth { get; set; } = 10f;
        private float _currentHealth;
        public float currentHealth { 
            get { return _currentHealth; } 
            set { if (_currentHealth != value) _currentHealth = value; 
                if (value < 0f) value = 0f; } }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }

        private void Start()
        {
            currentHealth = maxHealth;
        }
        private void Update()
        {
            if((backPlane.fillAmount - healthBar.fillAmount) > 0.001f)
            backPlane.fillAmount = Mathf.Lerp(backPlane.fillAmount, healthBar.fillAmount, Time.deltaTime * 10f);

            if (currentHealth <= 0) OnDeath();
        }
        public void GetHit(float demage)
        {
            currentHealth -= demage;
            healthBar.fillAmount = currentHealth / maxHealth;
        }

        public void OnDeath()
        {
            gameObject.SetActive(false);
        }
    }
}

