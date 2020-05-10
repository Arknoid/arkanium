using System;
using System.Collections;
using ScoreSpace.Interfaces;
using ScoreSpace.Managers;
using ScoreSpace.UI;
using UnityEngine;

namespace ScoreSpace.Core
{
    public class Projectile : MonoBehaviour, IProjectile
    {
        
        public int Power { get; set; }
        [SerializeField] private bool _destroyOnTrigger = true;
        [SerializeField] private int _power = 10;
        [SerializeField] private bool _isPlayerShoot = false;
        [SerializeField] private bool _autoDisable = true;
        [SerializeField] private float _autoDisableDelay = 2f;
        
        private void OnEnable()
        {
            Power = _power;
            if (_autoDisable)
            {
                StartCoroutine(Disable());
            }
        }

        private IEnumerator Disable()
        {
            yield return new WaitForSeconds(_autoDisableDelay);
            this.gameObject.SetActive(false);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            var otherCanDamage = other.gameObject.GetComponent<IDamageable>();
            otherCanDamage.TakeDamage(Power);
            if (_destroyOnTrigger)
            {
                this.gameObject.SetActive(false);
            }
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            var otherCanDamage = other.gameObject.GetComponent<IDamageable>();
            if (otherCanDamage !=null && otherCanDamage.TakeDamage(Power) && _isPlayerShoot)
            {
                // GameManager.Instance.PlayerScore += other.GetComponent<IScorePoints>().PointsToAdd;
                // var scoreObj = ObjectPooler.Instance.GetPooledObject("Score") ;
                // if (scoreObj != null)
                // {
                //     scoreObj.transform.position = new Vector3(other.transform.position.x,
                //         other.transform.position.y - 2, other.transform.position.z);
                //     scoreObj.GetComponent<ScoreObject>().ScoreValue = other.GetComponent<IScorePoints>().PointsToAdd;    
                //     scoreObj.SetActive(true);
                // }
            }
            
            if (_destroyOnTrigger)
            {
               this.gameObject.SetActive(false);
            }
        }
    }
}