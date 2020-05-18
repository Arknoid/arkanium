
using System.Collections;
using ScoreSpace.Core;
using ScoreSpace.Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScoreSpace.Managers
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        [SerializeField] private int _arkaniumNeed = 10;
        [SerializeField] private GameObject _player;
        [SerializeField] private AudioClip _winSound;
        [SerializeField] private AudioClip _gameOverSound;
        [SerializeField] private AudioClip _readySound;
        [SerializeField] private AudioClip[] _musics;
        private int _arkaniumCount;
        public int ArkaniumNeed
        {
            get => _arkaniumNeed;
            set => _arkaniumNeed = value;
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
            ArkaniumCount = 0;
            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(1.5f);
            SoundManager.Instance.PlaySingle(_readySound, true);
            yield return new WaitForSeconds(0.5f);
            SoundManager.Instance.PlayMusic(_musics[Random.Range(0,_musics.Length)]);
            _player.SetActive(true);
        }

    
        public void Win()
        {
            Time.timeScale = 0;
            GameManager.Instance.PlayerScore += ArkaniumNeed * 10;
            SoundManager.Instance.PlaySingle(_winSound, true);
            UiManager.Instance.ShowGameOver(true);
        }


        private IEnumerator CorLoose()  
        {
            yield return new WaitForSeconds(2f);
            SoundManager.Instance.PlaySingle(_gameOverSound, true);
            SoundManager.Instance.StopMusic();
            yield return new WaitForSeconds(1f);
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
            GameManager.Instance.PlayerScore = 0;  
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