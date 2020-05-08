using System.Collections;
using ScoreSpace.Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScoreSpace.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private float _playerDieSpawnDelay = 1f;
        
        private LevelManager _currentLevelManager;
        
        public int PlayerScore { get; set; }

        public LevelManager CurrentLevelManager
        {
            set
            {
                _currentLevelManager = value;
                StartCoroutine(SpawnPlayer());
            }
        }
        
        public void PlayerDie()
        {
            // TODO remove life
            // StartCoroutine(SpawnPlayer(_playerDieSpawnDelay));
            StartCoroutine(RestartLevel());
        }
        private IEnumerator RestartLevel()
        {
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private IEnumerator SpawnPlayer(float delay = 0)
        {
            
            yield return new WaitForSeconds(delay);
            _currentLevelManager.SpawnPlayer(_playerPrefab);
        }
        
    }
}