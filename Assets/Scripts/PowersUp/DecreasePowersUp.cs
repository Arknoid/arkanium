using ScoreSpace.Player;
using UnityEngine;

namespace ScoreSpace.PowersUp
{
    public class DecreasePowersUp : MonoBehaviour
    {
        private PlayerPowerUp _playerPowerUp;
        private PlayerMovement _playerMovement;
        private void OnTriggerEnter2D(Collider2D other)
        {
            _playerPowerUp = other.GetComponent<PlayerPowerUp>();
            _playerMovement = other.GetComponent<PlayerMovement>();
            if (_playerPowerUp == null) return;
            _playerPowerUp.ShieldLevel--;
            _playerPowerUp.SideShotLevel--;
            _playerMovement.SpeedBonus -= _playerPowerUp.SpeedToAdd;
            _playerPowerUp.LaserLevel--;
        }
    }
}