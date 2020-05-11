using System;
using ScoreSpace.Managers;
using UnityEngine;

namespace ScoreSpace
{
    public class Ship : UnityEngine.MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            if (GameManager.Instance.ArkaniumCount >= GameManager.Instance.ArkaniumNeed )
            {
                other.gameObject.SetActive(false);
                GameManager.Instance.PlayerScore += GameManager.Instance.ArkaniumNeed;
                _animator.SetTrigger("exit");
            }

        }

        public void SetEndGame()
        {
            GameManager.Instance.Win();
        }
    }   
}