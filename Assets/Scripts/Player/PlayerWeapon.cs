using System;
using System.Collections;
using ScoreSpace.Core;
using ScoreSpace.Interfaces;
using ScoreSpace.Managers;
using Unity.Mathematics;
using UnityEngine;

namespace ScoreSpace.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        
        [SerializeField] private GameObject[] _baseWeaponShots;

        [SerializeField] private Transform _baseWeaponMountPoint;
        [SerializeField] private Transform _beamWeaponMountPoint;
        [SerializeField] private Transform _sideShotMountPointLeft;
        [SerializeField] private Transform _sideShotMountPointRight;
        [SerializeField] private Transform _doubleBeamWeaponMountPoint1;
        [SerializeField] private Transform _doubleBeamWeaponMountPoint2;
        [SerializeField] private string _baseShotTag;
        [SerializeField] private string _sideShotsTag;
        [SerializeField] private string _beamShotTag;
        [SerializeField] private AudioClip _baseWeaponSound;
        [SerializeField] private AudioClip _beamWeaponSound;
        [SerializeField] private float _shootRate = 0.2f;
        [SerializeField] private Transform _weapon;
        private Rigidbody2D _rbWeapon;
        private Rigidbody2D _rb;
        
        private PlayerInput _playerInput;

        public float ShootRate
        {
            get => _shootRate;
            set => _shootRate = Mathf.Clamp(value, 0.05f, 0.5f);
        }

        private PlayerPowerUp _playerPowerUp;

        private PlayerMovement _playerMovement;
        private bool _canShoot = true;
        private static readonly int Power = Animator.StringToHash("power");

        private IEnumerator FireDelay()
        {
            _canShoot = false;
            yield return new WaitForSeconds(_shootRate);
            _canShoot = true;

        }
        private void Awake()
        {
            _playerInput = FindObjectOfType<PlayerInput>();
            _playerInput.OnFire += HandleFire;
            _playerInput.OnHoldFire += HandleBeamFire;
            _playerPowerUp = FindObjectOfType<PlayerPowerUp>();
            _rbWeapon = _weapon.GetComponent<Rigidbody2D>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector2 lookDir = (_playerInput.MousePos - _weapon.transform.position);
            var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg ;
            _weapon.transform.eulerAngles  = new Vector3(0f,0f,angle);
        }

        private void OnDestroy()
        {
            CleanEventsDelegate();
        }

        private void OnDisable()
        {
            CleanEventsDelegate();
        }
        
        private void CleanEventsDelegate()  
        {
            GetComponent<PlayerInput>().OnFire -= HandleFire;
            GetComponent<PlayerInput>().OnHoldFire -= HandleBeamFire;
        }
        
        private  void HandleFire()
        {

            if (!_canShoot) return;
            SpawnShot(_baseShotTag, _baseWeaponMountPoint.transform, transform.right , _playerPowerUp.LaserLevel , transform.rotation.eulerAngles.z);
            SoundManager.Instance.RandomizeSfx(_baseWeaponSound);
            if (_playerPowerUp.SideShotsLevel > 0)
            {
                SpawnSideShots();
            }
            StartCoroutine(FireDelay());
        }
        
        private void SpawnShot(string shotTag,Transform mountPoint, Vector2 direction,int power = 1,float eulerAngle = 0f)
        {
            var spawnedShot = ObjectPooler.Instance.GetPooledObject(shotTag);
            if (spawnedShot == null) return;
            spawnedShot.transform.position = mountPoint.position;
            spawnedShot.transform.rotation = mountPoint.rotation;
            spawnedShot.SetActive(true);
            spawnedShot.GetComponent<Animator>().SetInteger(Power, power);
        }
        private void SpawnSideShots()
        {
            // const float eulerAngle = 15f;
            // SpawnShot(_sideShotsTag,_sideShotMountPointLeft,
            //     GetDirectionByPlayerDirection(0.05f),_playerPowerUp.SideShotsLevel -1,transform.rotation.eulerAngles.z + eulerAngle);
            // SpawnShot(_sideShotsTag,_sideShotMountPointRight,
            //     GetDirectionByPlayerDirection(-0.05f),_playerPowerUp.SideShotsLevel -1,transform.rotation.eulerAngles.z -eulerAngle);
            //
            // // extra shots
            // if (_playerPowerUp.SideShotsLevel <= 3) return;
            // SpawnShot(_sideShotsTag,_sideShotMountPointLeft,
            //     GetDirectionByPlayerDirection(0.2f),1,transform.rotation.eulerAngles.z + eulerAngle);
            // SpawnShot(_sideShotsTag,_sideShotMountPointRight,
            //     GetDirectionByPlayerDirection(-0.2f),1,transform.rotation.eulerAngles.z -eulerAngle);
        }
        private void HandleBeamFire(bool isFull)
        {
            if (!_canShoot) return;
            if (isFull)
            {
                SpawnShot(_beamShotTag, _doubleBeamWeaponMountPoint1.transform,
                    Vector2.right, _playerPowerUp.LaserLevel , transform.rotation.eulerAngles.z);
                SpawnShot(_beamShotTag, _doubleBeamWeaponMountPoint2.transform,
                    Vector2.right, _playerPowerUp.LaserLevel , transform.rotation.eulerAngles.z);
            }
            else
            {
                SpawnShot(_beamShotTag, _beamWeaponMountPoint.transform,
                    Vector2.right, _playerPowerUp.LaserLevel , transform.rotation.eulerAngles.z);
            }
            SoundManager.Instance.RandomizeSfx(_beamWeaponSound);
            StartCoroutine(FireDelay());
        }
        
    }
}
