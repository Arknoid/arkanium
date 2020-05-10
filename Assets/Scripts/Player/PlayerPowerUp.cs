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
        [SerializeField] private AudioClip _soundArkanium;
        
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
                    other.gameObject.SetActive(false);
                    break;
                case "PowerUpHealth":
                    SoundManager.Instance.RandomizeSfx(_soundPowerUpEnergy);
                    _playerHealth.CurrentHealth += _healthToAdd;
                    other.gameObject.SetActive(false);
                    break;
                case "PowerUpShield":
                    ShieldLevel++;
                    SoundManager.Instance.RandomizeSfx(_soundPowerUpShield);
                    other.gameObject.SetActive(false);
                    break;
                case "PowerUpWeapon":
                    LaserLevel++;
                    SoundManager.Instance.RandomizeSfx(_soundPowerUpLaser);
                    other.gameObject.SetActive(false);
                    break;
                case "Arkanium":
                    GameManager.Instance.ArkaniumCount++;
                    SoundManager.Instance.RandomizeSfx(_soundArkanium);
                    other.gameObject.SetActive(false);
                    break;
            }
            
        }
    }
}