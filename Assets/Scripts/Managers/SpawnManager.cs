using System;
using System.Collections;
using System.Linq;
using ScoreSpace.Core;
using ScoreSpace.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScoreSpace.Managers
{
    public class SpawnManager : MonoBehaviour
    {

        [TagSelector]
        [SerializeField] private string[] _poolObjectsTag;
        [SerializeField] private GameObject _cameraSpawnPoints;
        [SerializeField] private float _spawnRate = 0.5f;
        private Transform[] _cameraSpawnsPos;
        private Transform[] _spawnsPos;
        [SerializeField] private float _spawnerSpawnRate = 60f;
        [SerializeField] private float _spanwerEnemy2Delay = 120f;
        [SerializeField] private float _spawnRateDecreaseDelay = 30f;
        
        
        private void Start()
        {
            _cameraSpawnsPos =_cameraSpawnPoints.gameObject.GetComponentsInChildren<Transform>();
            _spawnsPos = gameObject.GetComponentsInChildren<Transform>();
            StartCoroutine(StartSpawn());
            StartCoroutine(SpawnEnemySpawner());
            StartCoroutine(DecreaseSpawnRate());
        }

        private IEnumerator DecreaseSpawnRate()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnRateDecreaseDelay);
                _spawnRate -= 0.05f;
            }

        }
        

        private IEnumerator SpawnEnemySpawner()
        {
            yield return new WaitForSeconds(2);
            while (true)  
            {
                var spawnedObject = Time.time > _spanwerEnemy2Delay ? ObjectPooler.Instance.GetPooledObject("EnemySpawner2") : ObjectPooler.Instance.GetPooledObject("EnemySpawner");
                if (spawnedObject != null)
                {
                    var randIndex = Random.Range(0, _spawnsPos.Length);
                    var spawnPos = _spawnsPos[randIndex];
                    spawnedObject.transform.position = new Vector3(spawnPos.position.x, spawnPos.position.y,0);
                    spawnedObject.SetActive(true);
                }
                yield return new WaitForSeconds(_spawnerSpawnRate);
            }
            
        }
        private IEnumerator StartSpawn()
        {
            var nextIndex = 0;
            var randomSpawnPosIndex= Random.Range(0, _cameraSpawnsPos.Length);
            yield return new WaitForSeconds(2);
            while (true)
            {
                
                if (nextIndex > 6)
                {
                    randomSpawnPosIndex  = Random.Range(0, _cameraSpawnsPos.Length);
                    nextIndex = 0;
                }
                var finalIndex = randomSpawnPosIndex + Random.Range(-3,3);
                if (finalIndex >= _cameraSpawnsPos.Length || finalIndex <= 0)
                {
                    finalIndex = 0;
                }
                
                var spawnedObject = Time.time > _spanwerEnemy2Delay ? ObjectPooler.Instance.GetPooledObject("Enemy2") : ObjectPooler.Instance.GetPooledObject("Enemy");
                if (spawnedObject != null)
                {
                    var spawnPos = _cameraSpawnsPos[finalIndex];
                    spawnedObject.transform.position = new Vector3(spawnPos.position.x, spawnPos.position.y,0);
                    spawnedObject.SetActive(true);
                }

                nextIndex++;
                yield return new WaitForSeconds(_spawnRate);
            }

        }
        
    }
    
}