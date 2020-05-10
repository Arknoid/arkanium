using System;
using System.Collections;
using ScoreSpace.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScoreSpace.Core
{
    public class Spawner : MonoBehaviour
    {
        [TagSelector]
        [SerializeField] private string _objectTag;

        [SerializeField] private float _range = 4f;
        
        [SerializeField] private float _spawnRate;


        private void Start()
        {
            StartCoroutine(StartSpawn());
        }


        private IEnumerator StartSpawn()
        {
            yield return new WaitForSeconds(0.5f);
            while (true)
            {
                var randomPos = Random.insideUnitSphere * _range;
                var spawnedObject = ObjectPooler.Instance.GetPooledObject(_objectTag);
                if (spawnedObject != null)
                {
                    spawnedObject.transform.position = transform.position + randomPos;
                        spawnedObject.SetActive(true);
                }
                
                yield return new WaitForSeconds(_spawnRate);
            }

        }
    }
}