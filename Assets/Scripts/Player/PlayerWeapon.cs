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
        [SerializeField] private Transform _targetCursor;
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

        private bool _canShoot = true;
        private static readonly int Power = Animator.StringToHash("power");

        private Vector3 _prevMousePos = Vector3.zero;
        private float _angle;

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

        private void Start()
        {
            _prevMousePos = Input.mousePosition;
        }



        private void Update()
        {
            RotateWeapon();
        }


        private void RotateWeapon()
        {
            // Check if mouse is used
            if (!_prevMousePos.Equals(Input.mousePosition))
            {
                // Rotate with mouse
                var dir = _playerInput.CameraMousePos - _weapon.transform.position;
                _angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                _prevMousePos = Input.mousePosition;
                _targetCursor.gameObject.SetActive(false);
                Cursor.visible = true;
            }
            // Check if right analog axe of joypad is used
            if (Mathf.Abs(_playerInput.HorizontalShoot) > 0.01 && Mathf.Abs(_playerInput.VerticalShoot) > 0.01)
            {
                // Rotate with joypad
                _angle = Mathf.Atan2( - _playerInput.HorizontalShoot, _playerInput.VerticalShoot) * Mathf.Rad2Deg;
                _targetCursor.gameObject.SetActive(true);
                Cursor.visible = false;
            }
            _weapon.transform.eulerAngles = new Vector3(0f, 0f, _angle);
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

        private void HandleFire()
        {

            if (!_canShoot) return;
            SpawnShot(_baseShotTag, _baseWeaponMountPoint.transform, Vector2.zero, _playerPowerUp.LaserLevel, transform.rotation.eulerAngles.z);
            SoundManager.Instance.RandomizeSfx(_baseWeaponSound);
            if (_playerPowerUp.SideShotsLevel > 0)
            {
                SpawnSideShots();
            }
            StartCoroutine(FireDelay());
        }

        private void SpawnShot(string shotTag, Transform mountPoint, Vector2 direction = new Vector2(), int power = 1, float eulerAngle = 0f)
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
            const float eulerAngle = 15f;
            SpawnShot(_sideShotsTag, _sideShotMountPointLeft,
                Vector2.zero, _playerPowerUp.SideShotsLevel - 1);
            SpawnShot(_sideShotsTag, _sideShotMountPointRight,
                Vector2.zero, _playerPowerUp.SideShotsLevel - 1);
            
        }
        private void HandleBeamFire(bool isFull)
        {
            if (!_canShoot) return;
            if (isFull)
            {
                SpawnShot(_beamShotTag, _doubleBeamWeaponMountPoint1.transform,
                    Vector2.zero, _playerPowerUp.LaserLevel, transform.rotation.eulerAngles.z);
                SpawnShot(_beamShotTag, _doubleBeamWeaponMountPoint2.transform,
                    Vector2.zero, _playerPowerUp.LaserLevel, transform.rotation.eulerAngles.z);
            }
            else
            {
                SpawnShot(_beamShotTag, _beamWeaponMountPoint.transform,
                    Vector2.zero, _playerPowerUp.LaserLevel, transform.rotation.eulerAngles.z);
            }
            SoundManager.Instance.RandomizeSfx(_beamWeaponSound);
            StartCoroutine(FireDelay());
        }

    }
}
