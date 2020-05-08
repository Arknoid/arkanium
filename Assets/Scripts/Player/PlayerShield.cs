using UnityEngine;

namespace ScoreSpace.Player
{
    public class PlayerShield : MonoBehaviour
    {
        private PlayerPowerUp _playerPowerUp;
        private PlayerHealth _playerHealth;
        [SerializeField] private GameObject _shield;
        
        private Animator _shieldAnimator;
        private static readonly int Level = Animator.StringToHash("Level");

        private void Awake()
        {   
            _playerPowerUp = GetComponent<PlayerPowerUp>();
            _shieldAnimator = _shield.GetComponent<Animator>();
            _playerHealth = GetComponent<PlayerHealth>();
        }

        private void Update()
        {
            
            if (_playerPowerUp.ShieldLevel > 0 && !_playerHealth.IsDie)
            {
                _shield.SetActive(true);
                _shieldAnimator.SetInteger(Level, _playerPowerUp.ShieldLevel);
            }
            else
            {
                _shieldAnimator.Rebind();
                _shield.SetActive(false);
            }
        }
    }
}