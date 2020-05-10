using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;


namespace ScoreSpace.Core
{
    public class LightShine : MonoBehaviour
    {
        private Light2D _light;
        [SerializeField] private float _minIntensity = 0.1f;
        [SerializeField] private float _maxIntensity = 0.3f;
        [SerializeField] private float _rate = 0.2f;
    
        private void Awake()
        {
            _light = GetComponent<Light2D>();
        
        }

        private void Start()
        {
            StartCoroutine(StartShine());
        }

        private IEnumerator StartShine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_rate);
                _light.pointLightOuterRadius =(float) Random.Range(_minIntensity, _maxIntensity);
            }

        }
    }
}