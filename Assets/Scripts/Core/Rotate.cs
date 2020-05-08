using UnityEngine;
using Random = UnityEngine.Random;

namespace ScoreSpace.Core
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private int _speed = 100;
        [SerializeField] private bool _isRandom = false;

        [SerializeField] private int _minRandSpeed = -200;
        [SerializeField] private int _maxRandSpeed = 200;
        
        private void Start()
        {
            if (_isRandom)
            {
                _speed = Random.Range(_minRandSpeed, _maxRandSpeed);
            }
        }

        private void Update()
        { 
            transform.Rotate(Vector3.forward * (Time.deltaTime * _speed));
        }
        
        
    }
    
    
}