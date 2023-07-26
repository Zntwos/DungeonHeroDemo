using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using QFramework;
using UnityEngine.SceneManagement;

namespace DungeonHero
{
    public class PlayerController : MonoBehaviour,IController
    {
        [SerializeField]
        private float _moveSpeed = 2f;
        private Rigidbody2D _rigidbody;
        private Vector2 _move;
        private Animator _animator;
        private SpriteRenderer _sp;

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
            Debug.Log(LookX);
            if(LookX != 0f)
                _sp.flipX = LookX > 0f ? false : true;
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
        /// <summary>
        /// ´¥·¢Æ÷¼ì²â
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Reward")
            {
                Destroy(collision.gameObject);
                this.SendCommand<GoldScoreCommand>();
            }
            if(collision.gameObject.tag == "Door")
            {
                this.SendCommand<GamePassCommand>();
                SceneManager.LoadScene(1);
            }
        }

        public IArchitecture GetArchitecture()
        {
            return ModalsApp.Interface;
        }
    }
}
