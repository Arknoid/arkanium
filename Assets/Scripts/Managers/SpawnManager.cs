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
        [SerializeField] private float _spawnRate = 0.5f;
        private Transform[] _spawnsPos;
        private void Start()
        {
            _spawnsPos = gameObject.GetComponentsInChildren<Transform>();
            StartCoroutine(StartSpawn());
        }

        private IEnumerator StartSpawn()
        {
            var nextIndex = 0;
            var randomSpawnPosIndex= Random.Range(0, _spawnsPos.Length);
            yield return new WaitForSeconds(2);
            while (true)
            {
                
                if (nextIndex > 5)
                {
                    randomSpawnPosIndex  = Random.Range(0, _spawnsPos.Length);
                    nextIndex = 0;
                }
                var finalIndex = randomSpawnPosIndex + Random.Range(-3,3);
                if (finalIndex >= _spawnsPos.Length || finalIndex <= 0)
                {
                    finalIndex = 0;
                }

                Debug.Log(finalIndex);
                var spawnedObject = ObjectPooler.Instance.GetPooledObject(_poolObjectsTag[Random.Range(0, _poolObjectsTag.Length)]);
                if (spawnedObject != null)
                {
                    var spawnPos = _spawnsPos[finalIndex];
                    spawnedObject.transform.position = new Vector3(spawnPos.position.x, spawnPos.position.y,0);
                    spawnedObject.SetActive(true);
                }

                nextIndex++;
                yield return new WaitForSeconds(_spawnRate);
            }

        }
        
    }
    
}