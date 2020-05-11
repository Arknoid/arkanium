
using System;
using System.Collections;
using ScoreSpace.Interfaces;
using UnityEngine;

namespace ScoreSpace.PowersUp
{
    public class Shield : MonoBehaviour, IDamageable
    {

        [SerializeField] private int _damage = 40;
        [SerializeField] private int _health = 50;
        private SpriteRenderer _spriteRenderer;


        private void Awake()
        {
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
                Debug.Log(Health);
        }

        private void Update()
        {
            if (Health <= 0) gameObject.SetActive(false);
        }

        public bool TakeDamage(int damageTaken = 5)
        { 
            _health -= damageTaken;
            return false;
        }
        
    }
}