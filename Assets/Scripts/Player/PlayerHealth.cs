using System.Collections;
using ScoreSpace.Core;
using ScoreSpace.Managers;
using UnityEngine;

namespace ScoreSpace.Player
{
    public class PlayerHealth : Health
    {
        private Rigidbody2D _rb;
        private PlayerParticles _playerParticles;
        protected override void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _playerParticles = GetComponent<PlayerParticles>();
            base.Awake();
        }

        public override bool TakeDamage(int damageTaken)
        {
            CameraShake.Instance.StartShake();
            return base.TakeDamage(damageTaken);
        }
        
        protected override IEnumerator Explode()
        {
            
            _playerParticles.StopParticles();
            if (_rb!= null)_rb.simulated = false;
            GameManager.Instance.PlayerDie();
            yield return StartCoroutine(base.Explode());

            Destroy(this.gameObject);
        }
    }
}