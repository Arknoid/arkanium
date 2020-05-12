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
        [SerializeField] private Text _arkaniumText;
        [SerializeField] private Text _returnText;
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private AudioClip _gameOverMusic;
        [SerializeField] private AudioClip _winMusic;
        [SerializeField] private Text _gameOverText;
        [SerializeField] private GameObject _energyBar;
        [SerializeField] private Text _gameOverScoreText;
        [SerializeField] private Text _gameOverHiScoreText;
        [SerializeField] private GameObject _pausePanel;
        

        private PlayerPowerUp _playerPowerUp;
        private PlayerHealth _playerHealth;
        private PlayerWeapon _playerWeapon;
        private PlayerMovement _playerMovement;

        public GameObject Player
        {
            get => _player;
            set => _player = value;
        }

        public void ShowReturnToShipText(bool value = true)
        {
            _returnText.gameObject.SetActive(value);
        }
        
        private void Start()
        {
            _gameOverPanel.SetActive(false);
            _debugInfosText.gameObject.SetActive(_showDebugInfos);
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _playerPowerUp = _player.GetComponent<PlayerPowerUp>();
            _playerWeapon = _player.GetComponent<PlayerWeapon>();
            _playerMovement = _player.GetComponent<PlayerMovement>();
        }

        public void ShowGameOver(bool isWin, bool value = true)
        {
            _energyBar.gameObject.SetActive(false);
            _returnText.gameObject.SetActive(false);
            _scoreText.gameObject.SetActive(false);
            _gameOverPanel.SetActive(value);
            if (isWin)
            {
                SoundManager.Instance.PlayMusic(_winMusic);
                _gameOverText.text = "YOU WIN";
            }
            else
            {
                SoundManager.Instance.PlayMusic(_gameOverMusic);
            }

            if (GameManager.Instance.NewHiScore)
            {
                _gameOverHiScoreText.color = Color.yellow;
                   _gameOverHiScoreText.text = "New HiScore : " + GameManager.Instance.HiScore.ToString();
                   _gameOverScoreText.text = "";
            }       
            else
            {
                _gameOverHiScoreText.text = "HiScore : " + GameManager.Instance.HiScore.ToString();
                _gameOverScoreText.color = Color.red;
                _gameOverScoreText.text = "Score : " + GameManager.Instance.PlayerScore.ToString();
            }
        }

        public void Pause(bool value = true)
        {
            if (value)
            {
                Time.timeScale = 0;
                _pausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                _pausePanel.SetActive(false);
            }
        }
        
        private void Update()
        {
            if (Input.GetButton("Cancel") || Input.GetKeyDown(KeyCode.Escape))
            {
                Pause(true);
            }
            if (_player == null) return;
            _scoreText.text = "Score : " + GameManager.Instance.PlayerScore.ToString();
            _arkaniumText.text = GameManager.Instance.ArkaniumCount + "/" +
                                 GameManager.Instance.ArkaniumNeed;
            if (!_showDebugInfos) return;
            BuildDebugInfos();
        }

        private void BuildDebugInfos()
        {
            _debugInfosText.text =
                "Health :" + _playerHealth.CurrentHealth
                           + "\nLaser : " + _playerPowerUp.LaserLevel
                           + "\nShoot rate : " + _playerWeapon.ShootRate
                           + "\nSpeedBonus : " + _playerMovement.SpeedBonus
                           + "\nArkanium :" + GameManager.Instance.ArkaniumCount + "/" +
                           GameManager.Instance.ArkaniumNeed;
        }
        
    }
}