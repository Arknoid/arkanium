﻿using System;
using UnityEngine;

namespace ScoreSpace.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private PlayerInput _playerInput;
        private Animator _animator;
        [SerializeField]
        private float _baseSpeed;
        [SerializeField] private float _slowSpeedFireHolding = 50;
        private float _speed;
        private static readonly int XAxis = Animator.StringToHash("xAxis");

        [SerializeField] private int _speedBonusMax = 200;
        private float _speedBonus = 0;

        public float SpeedBonus
        {
            get => _speedBonus;
            set => _speedBonus = Mathf.Clamp(value, 0, _speedBonusMax );
        }
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _playerInput = GetComponent<PlayerInput>();
            _animator = GetComponent<Animator>();
            _speed = _baseSpeed;
        }

        private void Update()
        {
            RotatePlayer();
            CheckHoldingFireAndSlowSpeed();
        }

        private void RotatePlayer()
        {
            var inputThreshold = 0.2;
            if (_playerInput.Horizontal < -inputThreshold && _playerInput.Vertical > -inputThreshold &&
                _playerInput.Vertical < inputThreshold)
            {
                transform.rotation = Quaternion.Euler(0, 0, -180);
            }
            else if (_playerInput.Horizontal < -inputThreshold && _playerInput.Vertical < -inputThreshold)
            {
                transform.rotation = Quaternion.Euler(0, 0, -135);
            }
            else if (_playerInput.Horizontal > inputThreshold && _playerInput.Vertical > -inputThreshold &&
                     _playerInput.Vertical < inputThreshold)
            {
                transform.rotation = Quaternion.identity;
            }
            else if (_playerInput.Horizontal < -inputThreshold && _playerInput.Vertical > inputThreshold)
            {
                transform.rotation = Quaternion.Euler(0, 0, 135);
            }
            else if (_playerInput.Horizontal > inputThreshold && _playerInput.Vertical > inputThreshold)
            {
                transform.rotation = Quaternion.Euler(0, 0, 45);
            }
            else if (_playerInput.Horizontal > inputThreshold && _playerInput.Vertical < -inputThreshold)
            {
                transform.rotation = Quaternion.Euler(0, 0, -45);
            }
            else if (_playerInput.Vertical < -inputThreshold && _playerInput.Horizontal > -inputThreshold &&
                     _playerInput.Horizontal < inputThreshold)
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (_playerInput.Vertical > inputThreshold && _playerInput.Horizontal > -inputThreshold &&
                     _playerInput.Horizontal < inputThreshold)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
        }

        private void FixedUpdate()
        {
            _rb.velocity = new Vector2(_playerInput.Horizontal * _speed * Time.fixedDeltaTime, _playerInput.Vertical* _speed * Time.fixedDeltaTime);
        }
        
        private void CheckHoldingFireAndSlowSpeed()
        {
            if (_playerInput.IsFireHolding)
            {
                _speed = _baseSpeed - _slowSpeedFireHolding + SpeedBonus;
            }
            else
            {
                _speed = _baseSpeed + SpeedBonus;
            }
        }
    }
}
