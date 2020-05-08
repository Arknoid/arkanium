using UnityEngine;

namespace ScoreSpace.Player
{
    public class PlayerParticles : MonoBehaviour
    {
        private ParticleSystem _leftReactParticles;
        private ParticleSystem _rightReactParticles;
        [SerializeField] private GameObject _fireHoldingParticles;
        [SerializeField] private GameObject _fullFireHoldingParticles;
        
        private PlayerInput _playerInput;
        
        private void Awake()
        {
            _leftReactParticles = GameObject.Find("Reactor Particle Right").GetComponent<ParticleSystem>();
            _rightReactParticles = GameObject.Find("Reactor Particle Left").GetComponent<ParticleSystem>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {

            EnableBeamParticles();

            EnableReactorParticles();
        }

        private void EnableReactorParticles()
        {
            if (_playerInput.Vertical <= -0.5f)
            {
                StopParticles();
            }
            else if (_playerInput.Vertical >= 0 && !_leftReactParticles.isPlaying && !_rightReactParticles.isPlaying)
            {
                PlayParticles();
            }
        }

        private void EnableBeamParticles()
        {
            if (_playerInput.IsFireHolding && !_playerInput.IsFullFireHolding)
            {
                _fireHoldingParticles.SetActive(true);
                _fullFireHoldingParticles.SetActive(false);
            } else if (_playerInput.IsFireHolding && _playerInput.IsFullFireHolding)
            {
                _fireHoldingParticles.SetActive(false);
                _fullFireHoldingParticles.SetActive(true);
            }
            else
            {
                _fireHoldingParticles.SetActive(false);
                _fullFireHoldingParticles.SetActive(false);
            }
        }
        
        private void PlayParticles()
        {
            _leftReactParticles.Play();
            _rightReactParticles.Play();
        }

        public void StopParticles()
        {
            _leftReactParticles.Stop();
            _rightReactParticles.Stop();
        }
    }
}
