using System.Collections;
using ScoreSpace.Core;
using ScoreSpace.Managers;
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
            StartCoroutine(GameManager.Instance.RestartLevel());
            yield return StartCoroutine(base.Explode());
        }
    }
}