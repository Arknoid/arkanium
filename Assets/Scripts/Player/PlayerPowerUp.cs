using ScoreSpace.Managers;
using UnityEngine;

namespace ScoreSpace.Player
{
    public class PlayerPowerUp : MonoBehaviour
    {
        [SerializeField] private int  _shieldLevelMax = 5;
        [SerializeField] private int  _laserLevelMax = 5;
        [SerializeField] private int  _sideShotLevelMax = 5;
        [SerializeField] private int _healthToAdd = 10;
        [SerializeField] private float _shootDelayBonus = 0.5f;
        [SerializeField] private int _speedToAdd = 25;
        
        [SerializeField] private AudioClip _soundPowerUpSpeed;
        [SerializeField] private AudioClip _soundPowerUpEnergy;
        [SerializeField] private AudioClip _soundPowerUpShield;
        [SerializeField] private AudioClip _soundPowerUpLaser;

        private PlayerWeapon _playerWeapon;
        private PlayerMovement _playerMovement;

        public int SpeedToAdd => _speedToAdd;

        public int SideShotLevel
        {
            get => _sideShotLevel;
            set => _sideShotLevel = Mathf.Clamp(value,0,_sideShotLevelMax);
        }
        public int ShieldLevel
        {
            get => _shieldLevel;
            set => _shieldLevel = Mathf.Clamp(value,0,_shieldLevelMax);
        }
        public int LaserLevel
        {
            get => _laserLevel;
            set => _laserLevel = Mathf.Clamp(value,0,_laserLevelMax);
        }
        
        private int _laserLevel = 0;
        private int _shieldLevel = 0;
        private int _sideShotLevel = 0;

        private PlayerHealth _playerHealth;
        
        private void Start()
        {
            _playerHealth = GetComponent<PlayerHealth>();
            _playerWeapon = GetComponent<PlayerWeapon>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.gameObject.tag)   
            {
                case "PowerUpSpeed" :
                    _playerMovement.SpeedBonus += _speedToAdd;
                    _playerWeapon.ShootRate -= _shootDelayBonus;
                    SoundManager.Instance.RandomizeSfx(_soundPowerUpSpeed);
                    Destroy(other.gameObject);
                    break;
                case "PowerUpHealth":
                    SoundManager.Instance.RandomizeSfx(_soundPowerUpEnergy);
                    _playerHealth.CurrentHealth += _healthToAdd;
                    Destroy(other.gameObject);
                    break;
                case "PowerUpShield":
                    ShieldLevel++;
                    SoundManager.Instance.RandomizeSfx(_soundPowerUpShield);
                    Destroy(other.gameObject);
                    break;
                case "PowerUpWeapon":
                    LaserLevel++;
                    SoundManager.Instance.RandomizeSfx(_soundPowerUpLaser);
                    Destroy(other.gameObject);
                    break;
            }
            
        }
    }
}