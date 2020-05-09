using ScoreSpace.Patterns;
using ScoreSpace.Player;
using UnityEngine;
using UnityEngine.UI;

namespace ScoreSpace.Managers
{
    public class UiManager : MonoSingleton<UiManager>
    {
        [SerializeField] private Text _debugInfosText;
        [SerializeField] private bool _showDebugInfos = false;
        [SerializeField] private Text _scoreText;
        
        private PlayerPowerUp _playerPowerUp;
        private PlayerHealth _playerHealth;
        private PlayerWeapon _playerWeapon;
        private PlayerMovement _playerMovement;
        private GameObject _player;

        public GameObject Player
        {
            get => _player;
            set
            {
                _player = value;
                _playerHealth = value.GetComponent<PlayerHealth>();
                _playerPowerUp = value.GetComponent<PlayerPowerUp>();
                _playerWeapon = value.GetComponent<PlayerWeapon>();
                _playerMovement = value.GetComponent<PlayerMovement>();
            }
        }
        private void Start()
        {
            _debugInfosText.gameObject.SetActive(_showDebugInfos);
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        
        private void Update()
        {
            if (_player == null) return;
            // _scoreText.text = "Score : " + GameManager.Instance.PlayerScore.ToString();
            if (!_showDebugInfos) return;
            BuildDebugInfos();
        }

        private void BuildDebugInfos()
        {
            _debugInfosText.text =
                "Health :" + _playerHealth.CurrentHealth
                           + "\nLaser : " + _playerPowerUp.LaserLevel
                           + "\nSideShot : " + _playerPowerUp.SideShotLevel
                           + "\nShield : " + _playerPowerUp.ShieldLevel
                           + "\nShoot rate : " + _playerWeapon.ShootRate
                           + "\nSpeedBonus : " + _playerMovement.SpeedBonus;
        }
        
    }
}