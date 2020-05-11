using System;
using ScoreSpace.PowersUp;
using UnityEngine;

namespace ScoreSpace.Player
{
    public class PlayerShield : MonoBehaviour
    {
        [SerializeField] private Shield _shield;
        [SerializeField] private int _shieldHealth = 50;


        public void ActivateShield()
        {
            _shield.Health += _shieldHealth;
            _shield.gameObject.SetActive(true);
        }
        
    }
}