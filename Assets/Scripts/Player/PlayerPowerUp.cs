﻿using ScoreSpace.Managers;
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
        private PlayerShield _playerShield;
        public int SpeedToAdd => _speedToAdd;
        
        public int LaserLevel
        {
            get => _laserLevel;
            set => _laserLevel = Mathf.Clamp(value,1,_laserLevelMax);
        }
        
        private int _laserLevel = 1;

        private PlayerHealth _playerHealth;
        
        private void Start()
        {
            _playerHealth = GetComponent<PlayerHealth>();
            _playerWeapon = GetComponent<PlayerWeapon>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerShield = GetComponent<PlayerShield>();
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
                    SoundManager.Instance.RandomizeSfx(_soundPowerUpShield);
                    _playerShield.ActivateShield();
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