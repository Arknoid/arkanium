using System.Collections;
using ScoreSpace.Core;
using ScoreSpace.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScoreSpace.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform[] _topSpawnPos;
        [TagSelector]
        [SerializeField] private string[] _poolObjectsTag;
        [SerializeField] private float _spawnRate = 0.5f;
        [SerializeField] private Transform _playerStartPosition;

        private void Start()
        {
            GameManager.Instance.CurrentLevelManager = this;
            StartCoroutine(StartSpawn());
        }

        private IEnumerator StartSpawn()
        {
            yield return new WaitForSeconds(2);
            while (true)
            {
                var spawnedObject = ObjectPooler.Instance.GetPooledObject(_poolObjectsTag[Random.Range(0, _poolObjectsTag.Length)]);
                if (spawnedObject != null)
                {
                    spawnedObject.transform.position =
                        _topSpawnPos[Random.Range(0, _topSpawnPos.Length)].transform.position;
                    spawnedObject.SetActive(true);
                }
                
                yield return new WaitForSeconds(_spawnRate);
            }

        }

        public void SpawnPlayer(GameObject player)
        {
            
            UiManager.Instance.Player = Instantiate(player, _playerStartPosition.position, Quaternion.identity);
        }
        
    }
    
}