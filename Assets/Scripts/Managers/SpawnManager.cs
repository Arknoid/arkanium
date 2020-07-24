
using System.Collections;
using ScoreSpace.Core;
using ScoreSpace.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScoreSpace.Managers
{
    public class SpawnManager : MonoBehaviour
    {

        private Transform[] _spawnsPos;
        [SerializeField] private float _spawnerSpawnRate = 60f;
        [SerializeField] private float _spanwerEnemy2Delay = 120f;
        
        private void Start()
        {
            _spawnsPos = gameObject.GetComponentsInChildren<Transform>();
            StartCoroutine(SpawnEnemySpawner());
        }

        private IEnumerator SpawnEnemySpawner()
        {
            yield return new WaitForSeconds(2);
            while (true)  
            {
                var spawnedObject = Time.timeSinceLevelLoad > _spanwerEnemy2Delay ? ObjectPooler.Instance.GetPooledObject("EnemySpawner2") : ObjectPooler.Instance.GetPooledObject("EnemySpawner");
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
        
    }
    
}