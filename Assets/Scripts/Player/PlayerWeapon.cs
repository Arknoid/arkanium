using System.Collections;
using ScoreSpace.Core;
using ScoreSpace.Interfaces;
using ScoreSpace.Managers;
using UnityEngine;

namespace ScoreSpace.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerWeapon : MonoBehaviour
    {
        
        [SerializeField] private GameObject[] _baseWeaponShots;

        [SerializeField] private Transform _baseWeaponMountPoint;
        [SerializeField] private Transform _beamMountPoint1;
        [SerializeField] private Transform _beamMountPoint2;
        [SerializeField] private Transform _sideShotMountPointLeft;
        [SerializeField] private Transform _sideShotMountPointRight;
        [SerializeField] private GameObject[] _beamLasers = new GameObject[5];
        [SerializeField] private GameObject[] _doubleBeamLasers = new GameObject[5];
        [SerializeField] private string _sideShotTag;
        [SerializeField] private string _baseShotTag;
        [SerializeField] private AudioClip _beamSound;
        [SerializeField] private AudioClip _baseWeaponSound;
        [SerializeField] private float _shootRate = 0.2f;

        public float ShootRate
        {
            get => _shootRate;
            set => _shootRate = Mathf.Clamp(value, 0.05f, 0.5f);
        }

        private PlayerPowerUp _playerPowerUp;

        private bool _canShoot = true;
        private static readonly int Power = Animator.StringToHash("Power");

        private IEnumerator FireDelay()
        {
            _canShoot = false;
            yield return new WaitForSeconds(_shootRate);
            _canShoot = true;

        }
        private void Awake()
        {
            GetComponent<PlayerInput>().OnFire += HandleFire;
            GetComponent<PlayerInput>().OnHoldFire += HandleBeamFire;
            _playerPowerUp = GetComponent<PlayerPowerUp>();
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
            SpawnShot(_baseShotTag, _baseWeaponMountPoint.transform, new Vector2(0f, 1f), _playerPowerUp.LaserLevel);
            SoundManager.Instance.RandomizeSfx(_baseWeaponSound);
            if (_playerPowerUp.SideShotLevel > 0)
            {
                SpawnSideShots();
            }
            StartCoroutine(FireDelay());
        }

        private void SpawnSideShots()
        {
            const float eulerAngle = 15f;
            SpawnShot(_sideShotTag,_sideShotMountPointLeft,
                new Vector2(-0.2f, 1f),_playerPowerUp.SideShotLevel -1,eulerAngle);
            SpawnShot(_sideShotTag,_sideShotMountPointRight,
                new Vector2(0.2f, 1f),_playerPowerUp.SideShotLevel -1,-eulerAngle);
            
            // extra shots
            if (_playerPowerUp.SideShotLevel <= 4) return;
            SpawnShot(_sideShotTag,_sideShotMountPointLeft,new Vector2(-0.5f, 1f),1,eulerAngle);
            SpawnShot(_sideShotTag,_sideShotMountPointRight,new Vector2(0.5f, 1f),1,-eulerAngle);
        }

        private void SpawnShot(string shotTag,Transform mountPoint, Vector2 direction,int power = 1,float eulerAngle = 0f)
        {
            var spawnedShot = ObjectPooler.Instance.GetPooledObject(shotTag);
            if (spawnedShot == null) return;
            spawnedShot.transform.position = mountPoint.position;
            spawnedShot.transform.rotation = Quaternion.Euler(0, 0, eulerAngle);
            spawnedShot.SetActive(true);
            spawnedShot.GetComponent<IMovable>().Direction = direction;
            spawnedShot.GetComponent<Animator>().SetInteger(Power, power);
        }

        private void HandleBeamFire(bool isFull)
        {
            var beamLevelIndex = _playerPowerUp.LaserLevel;
            if (beamLevelIndex > _beamLasers.Length -1) beamLevelIndex = _beamLasers.Length -1;
            var doubleBeamLevelIndex = _playerPowerUp.LaserLevel;
            if (doubleBeamLevelIndex > _doubleBeamLasers.Length -1) doubleBeamLevelIndex = _doubleBeamLasers.Length -1;
            SoundManager.Instance.RandomizeSfx(_beamSound);

            var leftBeam = Instantiate( isFull ?  _doubleBeamLasers[doubleBeamLevelIndex] : _beamLasers[beamLevelIndex], _beamMountPoint1.transform.position, Quaternion.identity);
            var rightBeam = Instantiate(isFull ?  _doubleBeamLasers[doubleBeamLevelIndex] : _beamLasers[beamLevelIndex], _beamMountPoint2.transform.position, Quaternion.identity);
            
        }
        
    }
}
