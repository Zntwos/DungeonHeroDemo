using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

namespace DungeonHero
{
    public class WeaponCtrl : MonoBehaviour, IController
    {
        public Weapon weapon;
        private bool _isActive;
        private Collider2D _collider2D;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private IObjectPoolSystem _objectPool;
        private Vector2 _weaponDir;

        private bool fire = false;
        private bool canFire = true;

        private Vector2 _trans1;
        private Vector2 _trans2;
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
            _trans1 = transform.position;
            //����������ȡʱ
            this.RegisterEvent<GetWeaponEvent>(e =>
            {
                //�ϴ�ͼ�굽����ģ��
                if(this.GetModel<IWeaponModal>().weaponHeld.Value == gameObject)
                {
                    this.GetModel<ISpriteModal>().weaponIcon.Value = weapon.icon;
                    _isActive = true;
                }
                    
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            //������������ʱ
            this.RegisterEvent<LoseWeaponEvent>(e =>
            {
                if(this.GetModel<IWeaponModal>().weaponHeld.Value == gameObject)
                {
                    //������ģ����ɾ��ͼ��
                    this.GetModel<ISpriteModal>().weaponIcon.Value = null;
                    //var pos = transform.parent.transform.position;
                    //transform.position = pos;
                    _isActive = false;
                }
                
                    
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            _collider2D.enabled = !_isActive;
            if (!_isActive) WeaponNotOnActive();
            else WeaponOnActive();
        }
        /// <summary>
        /// δ����״̬��
        /// </summary>
        private void WeaponNotOnActive()
        {
            //_trans2 = _trans1;
            //_trans2.y = Mathf.Sin(Time.fixedTime * Mathf.PI * rate) * amplitude + _trans1.y;
            //transform.position = _trans2;
        }
        /// <summary>
        /// ����״̬��
        /// </summary>
        private void WeaponOnActive()
        {
            if (Input.GetMouseButtonDown(0) && canFire)
            {
                fire = true;
                StartCoroutine(Timer());
                this.SendCommand<WeaponFire>();
                
                //��������ΪԶ��ʱ����
                if(weapon.weaponType == Weapon.weaponTypes.Gun)
                _objectPool.Get("Item/Bullet", o =>
                {
                    o.transform.position = transform.Find("Muzzle").position;
                    o.transform.right = _weaponDir;
                });
                
            }
            _animator.SetBool("Fire", fire);
            fire = false;
            //�������ҷ�ת
            var LookX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            if (LookX != 0f)
                _spriteRenderer.flipY = LookX > 0f ? false : true;
            //����������귽��
            _weaponDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            if (weapon.weaponType == Weapon.weaponTypes.Gun)
                transform.right = _weaponDir;
            else transform.parent.transform.right = _weaponDir;
        }

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.tag == "Player")
        //    {
        //        if(Input.GetKeyDown(KeyCode.F))
        //        {
        //            //�������󶨵���ɫ��
        //            this.transform.parent = collision.transform.Find("WeaponSlot");
        //            this.transform.position = collision.transform.Find("WeaponSlot").position;
        //            this.SendCommand<WeaponChangeCommand>();

        //            _isActive = true;
        //        }
                
        //    }
        //}
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
    }
}

