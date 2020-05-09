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
        [SerializeField] private Transform _beamWeaponMountPoint;
        [SerializeField] private Transform _doubleBeamWeaponMountPoint1;
        [SerializeField] private Transform _doubleBeamWeaponMountPoint2;
        [SerializeField] private string _baseShotTag;
        [SerializeField] private string _beamShotTag;
        [SerializeField] private AudioClip _baseWeaponSound;
        [SerializeField] private AudioClip _beamWeaponSound;
        [SerializeField] private float _shootRate = 0.2f;

        public float ShootRate
        {
            get => _shootRate;
            set => _shootRate = Mathf.Clamp(value, 0.05f, 0.5f);
        }

        private PlayerPowerUp _playerPowerUp;
        private PlayerInput _playerInput;
        private PlayerMovement _playerMovement;
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
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.OnFire += HandleFire;
            _playerInput.OnHoldFire += HandleBeamFire;
            _playerPowerUp = GetComponent<PlayerPowerUp>();
            _playerMovement = GetComponent<PlayerMovement>();
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
            SpawnShot(_baseShotTag, _baseWeaponMountPoint.transform, GetDirectionByPlayerDirection(), 1 , transform.rotation.eulerAngles.z);
            SoundManager.Instance.RandomizeSfx(_baseWeaponSound);
            StartCoroutine(FireDelay());
        }

        private Vector2 GetDirectionByPlayerDirection()
        {
            Vector2 direction;
            switch (_playerMovement.CurrentPosition)
            {
                case Position.Right:
                    direction = Vector2.right;
                    break;
                case Position.BottomRight:
                    direction = new Vector2(1, -1);
                    break;
                case Position.Bottom:
                    direction = Vector2.down;
                    break;
                case Position.BottomLeft:
                    direction = new Vector2(-1, -1);
                    break;
                case Position.Left:
                    direction = Vector2.left;
                    break;
                case Position.TopLeft:
                    direction = Vector2.right;
                    direction = new Vector2(-1, 1);
                    break;
                case Position.Top:
                    direction = Vector2.up;
                    break;
                case Position.TopRight:
                    direction = new Vector2(1, 1);
                    break;
                default:
                    direction = Vector2.zero;
                    break;
            }

            return direction;
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
            if (!_canShoot) return;
            if (isFull)
            {
                SpawnShot(_beamShotTag, _doubleBeamWeaponMountPoint1.transform, GetDirectionByPlayerDirection(), 1 , transform.rotation.eulerAngles.z);
                SpawnShot(_beamShotTag, _doubleBeamWeaponMountPoint2.transform, GetDirectionByPlayerDirection(), 1 , transform.rotation.eulerAngles.z);
            }
            else
            {
                SpawnShot(_beamShotTag, _beamWeaponMountPoint.transform, GetDirectionByPlayerDirection(), 1 , transform.rotation.eulerAngles.z);
            }
            SoundManager.Instance.RandomizeSfx(_beamWeaponSound);
            StartCoroutine(FireDelay());
        }
        
    }
}
