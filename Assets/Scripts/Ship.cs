using System;
using ScoreSpace.Managers;
using UnityEngine;

namespace ScoreSpace
{
    public class Ship : UnityEngine.MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            if (GameManager.Instance.ArkaniumCount >= GameManager.Instance.ArkaniumNeed )
            {
                Debug.Log("WIN");
                Time.timeScale = 0;
            }

        }
    }   
}