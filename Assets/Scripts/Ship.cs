using System;
using ScoreSpace.Core;
using ScoreSpace.Managers;
using UnityEngine;

namespace ScoreSpace
{
    public class Ship : UnityEngine.MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private AudioClip _landingSound;
        [SerializeField] private float _landingShakeDuration = 1f;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            SoundManager.Instance.PlaySingle(_landingSound, true);
            CameraShakeCinemachine.Instance.ShakeDuration = _landingShakeDuration;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            if (LevelManager.Instance.ArkaniumCount >= LevelManager.Instance.ArkaniumNeed )
            {
                other.gameObject.SetActive(false);
                SoundManager.Instance.StopMusic();
                SoundManager.Instance.PlaySingle(_landingSound, true);

                CameraShakeCinemachine.Instance.ShakeDuration = _landingShakeDuration;
                _animator.SetTrigger("exit");
            }

        }

        public void SetEndGame()
        {
            LevelManager.Instance.Win();
        }
    }   
}