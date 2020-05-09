using System;
using ScoreSpace.Interfaces;
using UnityEngine;

namespace ScoreSpace
{
    public class TargetShot : MonoBehaviour, IProjectile
    {
        public int Power { get; set; }
        [SerializeField] private int _power = 10;
        [SerializeField] private float _speed = 2;
        [SerializeField] private string _targetTag = "Player";
        private Rigidbody2D _rb;
        
        private GameObject _target;
        private void Awake()
        {
            Power = _power;
            _rb = GetComponent<Rigidbody2D>();
        }   

        private void OnEnable()
        {
            _target = GameObject.FindGameObjectWithTag(_targetTag);
            if (_target == null)
            {
                this.gameObject.SetActive(false);
                return;
            }
            _rb.velocity = (_target.transform.position -_rb.transform.position).normalized * _speed;
        }

        private void Update()
        {
            _rb.velocity = (_target.transform.position -_rb.transform.position).normalized * _speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.GetComponent<IDamageable>()?.TakeDamage(Power);
            this.gameObject.SetActive(false);
        }
    }
}