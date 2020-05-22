
using System;
using ScoreSpace.Interfaces;
using UnityEngine;

namespace ScoreSpace.PowersUp
{
    public class Shield : MonoBehaviour, IDamageable
    {

        [SerializeField] private int _damage = 40;
        [SerializeField] private int _health = 50;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public int Health
        {
            get => _health;
            set => _health = value <= 0 ? 0 : value;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.GetComponent<IDamageable>()?.TakeDamage(_damage);
            TakeDamage();
        }

        private void Update()
        {
            if (Health <= 0) gameObject.SetActive(false); 
            _animator.SetInteger("power", _health);
        }

        public bool TakeDamage(int damageTaken = 5)
        { 
            _health -= damageTaken;
            _animator.SetTrigger("hit");
            return false;
        }
        
    }
}