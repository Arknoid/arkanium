using System.Collections;
using ScoreSpace.Managers;
using UnityEngine;

namespace ScoreSpace.Core
{
    public class Weapon : MonoBehaviour
    {
        
        [SerializeField]
        private string _shotTag;
        [SerializeField]
        private AudioClip _weaponSound;
        [SerializeField]
        private Transform[] _weaponMountPoint;

        [SerializeField] private float _shootRate = 0.2f;

        protected bool _canShoot = true;


        protected virtual void OnEnable()
        {
            _canShoot = true;
        }

        private IEnumerator FireDelay()
        {
            _canShoot = false;
            yield return new WaitForSeconds(_shootRate);
            _canShoot = true;

        }
        protected virtual void HandleFire()
        {
            if (!_canShoot) return;
            foreach (var weaponMountPoint in _weaponMountPoint)
            {
                var spawnedShot = ObjectPooler.Instance.GetPooledObject(_shotTag);
                spawnedShot.transform.position = weaponMountPoint.position;
                spawnedShot.SetActive(true);
            }
           
            SoundManager.Instance.RandomizeSfx(_weaponSound, true);
            StartCoroutine(FireDelay());

        }
        
    }
}