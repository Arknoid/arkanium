using System.Collections;
using ScoreSpace.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        protected override IEnumerator Explode()
        {
            
            // _playerParticles.StopParticles();
            if (_rb!= null)_rb.simulated = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            yield return StartCoroutine(base.Explode());
            
        }
    }
}