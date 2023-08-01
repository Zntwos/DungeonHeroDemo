using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using QFramework;
using UnityEngine.SceneManagement;

namespace DungeonHero
{
    public class PlayerCtrl : MonoBehaviour,IController
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
        /// <summary>
        /// 触发器检测
        /// </summary>
        /// <param name="collision"></param>
        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.gameObject.tag == "Reward")
        //    {
        //        Destroy(collision.gameObject);
        //        this.SendCommand<GoldScoreCommand>();
        //    }
        //    else if(collision.gameObject.tag == "Door")
        //    {
        //        this.SendCommand<GamePassCommand>();
        //        //切换场景要做的事情
        //        this.GetSystem<IObjectPoolSystem>().Dispose();
        //        SceneManager.LoadScene(1);
        //    }
        //}
        //private void OnTriggerStay2D(Collider2D collision)
        //{
        //    if (collision.gameObject.tag == "Weapon")
        //    {
        //        if (Input.GetKeyDown(KeyCode.F))
        //        {
        //            if (_weapon.weaponHeld.Value == null)
        //            {
        //                //    将武器上传到模型
        //                //    _weapon.weaponHeld.Value = collision.gameObject;

        //                //    将武器绑定到角色
        //                //    collision.gameObject.transform.parent = transform.Find("WeaponSlot");
        //                //    collision.gameObject.transform.position = transform.Find("WeaponSlot").position;
        //                //    发送武器切换的命令
        //                //    this.SendCommand<GetWeaponCommand>();
        //                //
        //                BindWeapon(collision.gameObject);
        //            }
        //            else
        //            {
        //                //this.SendCommand<loseWeaponCommand>();
        //                //_weapon.weaponHeld.Value = collision.gameObject;
        //                UntieWeapon();
        //                BindWeapon(collision.gameObject);
        //            }

        //        }
        //    }
        //}
        //private void BindWeapon(GameObject obj)
        //{
        //    _weapon.weaponHeld.Value = obj;

        //    //将武器绑定到角色
        //    obj.transform.parent = transform.Find("WeaponSlot");
        //    obj.gameObject.transform.position = transform.Find("WeaponSlot").position;
        //    //发送武器切换的命令
        //    this.SendCommand<GetWeaponCommand>();
        //}
        //private void UntieWeapon()
        //{
        //    this.SendCommand<loseWeaponCommand>();
        //    var obj = _weapon.weaponHeld.Value;
        //    obj.transform.parent = null;
        //    obj.transform.position = transform.position;
        //    _weapon.weaponHeld.Value = null;
            
        //}


        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}
