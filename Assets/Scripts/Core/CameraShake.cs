using ScoreSpace.Patterns;
using UnityEngine;

namespace ScoreSpace.Core
{
    public class CameraShake : MonoSingleton<CameraShake>
    {

        private Transform _transform;
        public float shakeAmount = 0.1f;
        private Vector3 _originalPos;
        private float _shakeDuration;

        public void StartShake(float duration = 0.5f)
        {
            _shakeDuration = duration;
        }
        
        
        protected override void Init()
        {
            _transform = GetComponent(typeof(Transform)) as Transform;
        }
	
        private void OnEnable()
        {
            _originalPos = _transform.localPosition;
        }

        private void Update()
        {
            if (_shakeDuration > 0)
            {
                _transform.localPosition = _originalPos + Random.insideUnitSphere * shakeAmount;
			
                _shakeDuration -= Time.deltaTime;
            }
            else
            {
                _shakeDuration = 0f;
                _transform.localPosition = _originalPos;
            }
        }
    }
}