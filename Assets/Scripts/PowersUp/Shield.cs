using System.Collections;
using ScoreSpace.Player;
using UnityEngine;

namespace ScoreSpace.PowersUp
{
    public class Shield : MonoBehaviour
    {
        private PlayerPowerUp _playerPowerUp;
        private Animator _animator;
        [SerializeField] private float _decreaseTimer = 1f;
        private static readonly int Hit = Animator.StringToHash("Hit");

        private bool _isDecreasing;

        private void Awake()
        {
            _playerPowerUp = FindObjectOfType<PlayerPowerUp>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _animator.Rebind();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.SetActive(false);
            if(_playerPowerUp == null || _isDecreasing) return;
            StartCoroutine(HitAndDecrease());
        }

        private IEnumerator HitAndDecrease()
        {
            _animator.SetTrigger(Hit);
            _isDecreasing = true;
            yield return  new WaitForSeconds(_decreaseTimer);
            _playerPowerUp.ShieldLevel--;
            _isDecreasing = false;
        }
    }
}