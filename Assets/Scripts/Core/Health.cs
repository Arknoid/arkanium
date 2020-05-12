using System.Collections;
using ScoreSpace.Interfaces;
using ScoreSpace.Managers;
using ScoreSpace.UI;
using UnityEngine;

namespace ScoreSpace.Core
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _health = 3;
        [SerializeField] private SliderBar _energyBar;
        [SerializeField] private string _animatorTriggerHit = "hit";
        [SerializeField] private string _animatorTriggerExplode = "explode";
        [SerializeField] private AudioClip _soundHit;
        [SerializeField] private AudioClip _soundExplode;
        [SerializeField] private int _scoreValue;
        [SerializeField] private bool _forceSoundExplode = false;

        public int ScoreValue => _scoreValue;

        public bool IsDie => _currentHealth <= 0;
        private int _currentHealth;
        private bool _hasHealthBar;
        
        public int CurrentHealth
        {
            get => _currentHealth;
            set
            { 
                _currentHealth = Mathf.Clamp(value, 0, _health);
                if (_hasHealthBar)
                {
                    _energyBar.SetValue(_currentHealth);
                }
            } 
        }
        private Animator _animator;
        private Collider2D _collider;

        protected void Start()
        {
            _hasHealthBar = _energyBar != null;
            CurrentHealth = _health;
        }

        protected void OnEnable()
        {
            if (_hasHealthBar)
            {
                _energyBar.gameObject.SetActive(true);
            }
            CurrentHealth = _health;
            _animator.Rebind();
        }
        

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
        }

        public virtual bool TakeDamage(int damageTaken)
        {
            if (_soundHit != null)
            {
                SoundManager.Instance.PlaySingle(_soundHit);
            }
            CurrentHealth -= damageTaken;
            _animator.SetTrigger(_animatorTriggerHit);
            if (CurrentHealth > 0) return false;
            GameManager.Instance.PlayerScore += _scoreValue;
            StartCoroutine(Explode());
            return true;
        }

        protected virtual IEnumerator Explode()
        {
            if (_hasHealthBar)
            {
                _energyBar.gameObject.SetActive(false);
            }
            _collider.enabled = false;
            if (_soundExplode != null)
            {
                SoundManager.Instance.PlaySingle(_soundExplode, _forceSoundExplode);
            }
            _animator.SetTrigger(_animatorTriggerExplode);
            yield return new WaitForSeconds(1);
            this.gameObject.SetActive(false);
            
        }


    }
    
}