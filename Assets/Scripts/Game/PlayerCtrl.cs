using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using QFramework;
using UnityEngine.SceneManagement;

namespace DungeonHero
{
    public class PlayerCtrl : MonoBehaviour,IController,IGetHit
    {
        [SerializeField]
        private float _moveSpeed = 2f;
        private Rigidbody2D _rigidbody;
        private Vector2 _move;
        private Animator _animator;
        private SpriteRenderer _sp;

        public float maxHealth { get; set; } = 10f;
        private float _currentHealth;
        public float currentHealth
        {
            get { return _currentHealth; }
            set
            {
                if (_currentHealth != value) _currentHealth = value;
                if (value < 0f) value = 0f;
            }
        }
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _sp = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _move.x = Input.GetAxisRaw("Horizontal");
            _move.y = Input.GetAxisRaw("Vertical");
            AnimCtrl();
            var LookX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            if(LookX != 0f)
                _sp.flipX = LookX > 0f ? false : true;

            //if (Input.GetKeyDown(KeyCode.Q)) UntieWeapon();
        }
        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + _move.normalized * _moveSpeed * Time.fixedDeltaTime);
        }
        private void AnimCtrl()
        {
            var a = _move.magnitude == 0f ? false : true;
            _animator.SetBool("run", a);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out IReward reward))
            {
                reward.PickedUp(gameObject);
            }
        }


        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }

        public void GetHit(float damage)
        {
            
        }

        public void OnDeath()
        {
            
        }
    }
}
