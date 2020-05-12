
using System.Collections;
using ScoreSpace.Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScoreSpace.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private int _arkaniumNeed = 10;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _player;
        private int _arkaniumCount;
        public int ArkaniumNeed
        {
            get => _arkaniumNeed;
            set => _arkaniumNeed = value;
        }


        public bool NewHiScore { get; private set; } = false;

        private int _playerScore;
        private int _hiScore;
        public int PlayerScore
        {
            get => _playerScore;
            set
            {
                _playerScore = value;
                if (_playerScore > HiScore)
                {
                    HiScore = value;
                }
            }
        }

        public int HiScore
        {
            get => PlayerPrefs.GetInt("HiScore", 0);
            private set
            {
                PlayerPrefs.SetInt("HiScore", value);
                NewHiScore = true;
            } 
        }
        public int ArkaniumCount
        {
            get => _arkaniumCount;
            set
            {
                _arkaniumCount = value;
                if (_arkaniumCount >= _arkaniumNeed)
                {
                    UiManager.Instance.ShowReturnToShipText();
                }
            }
        }

        private void Start()
        {
            PlayerScore = 0;
            ArkaniumCount = 0;
            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(1.5f);
            _player.SetActive(true);
        }

    
        public void Win()
        {
            Time.timeScale = 0;
            UiManager.Instance.ShowGameOver(true);
        }


        private IEnumerator CorLoose()  
        {
            yield return new WaitForSeconds(2f);
            Time.timeScale = 0;
            UiManager.Instance.ShowGameOver(false);
        }
        
        public void Loose()
        {
            StartCoroutine(CorLoose());
        }

        
        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        public void Exit()
        {
#if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }

    }
}