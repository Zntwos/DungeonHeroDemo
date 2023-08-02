using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

namespace DungeonHero
{
    public class WeaponCtrl : MonoBehaviour, IController, IWeapon
    {
        public GameObject KeyUI;
        public Weapon weapon;
        private bool _isActive;
        private Collider2D _collider2D;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private IObjectPoolSystem _objectPool;
        private Vector2 _weaponDir;
        

        private bool fire = false;
        private bool canFire = true;

        [Range(0, 5f)]
        public float rate;
        [Range(0, 5f)]
        public float amplitude;

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
            _collider2D = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _objectPool = this.GetSystem<IObjectPoolSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            _collider2D.enabled = !_isActive;
            if (_isActive)WeaponOnActive();
        }
        /// <summary>
        /// 激活状态下
        /// </summary>
        private void WeaponOnActive()
        {
            if (Input.GetMouseButtonDown(0) && canFire)
            {
                fire = true;
                StartCoroutine(Timer());
                this.SendCommand<WeaponFire>();
                
                //仅当武器为远程时启用
                if(weapon.weaponType == Weapon.weaponTypes.Gun)
                _objectPool.Get("Item/Bullet", o =>
                {
                    o.transform.position = transform.Find("Muzzle").position;
                    o.transform.right = _weaponDir;
                });
                
            }
            _animator.SetBool("Fire", fire);
            fire = false;
            //武器左右翻转
            var LookX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            if (LookX != 0f)
                _spriteRenderer.flipY = LookX > 0f ? false : true;
            //武器朝向鼠标方向
            _weaponDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            if (weapon.weaponType == Weapon.weaponTypes.Gun)
                transform.right = _weaponDir;
            else transform.parent.transform.right = _weaponDir;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Player")
                KeyUI.SetActive(true);
        }
        void OnTriggerExit2D()
        {
            KeyUI.SetActive(false);
        }
        IEnumerator Timer()
        {
            canFire = false;
            yield return new WaitForSeconds(weapon.attackInterval);
            canFire = true;
        }
        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
        /// <summary>
        /// 武器被捡起
        /// </summary>
        /// <param name="Receiver">接收者</param>
        public void PickedUp(GameObject Receiver)
        {
            //Debug.Log(Receiver.name);
            this.transform.parent = Receiver.transform.Find("WeaponSlot");
            this.transform.position = Receiver.transform.Find("WeaponSlot").position;
            _isActive = true;
        }
        /// <summary>
        /// 武器被丢弃
        /// </summary>
        public void disposed()
        {
            transform.parent = null;
            _isActive = false;
        }
    }
}

