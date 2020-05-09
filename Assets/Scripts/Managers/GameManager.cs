
using System.Collections;
using ScoreSpace.Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScoreSpace.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private int _arkaniumNeed = 10;

        public int ArkaniumNeed
        {
            get => _arkaniumNeed;
            set => _arkaniumNeed = value;
        }
        public int PlayerScore { get; set; }
        public int ArkaniumCount { get; set; }

        private void Start()
        {
            PlayerScore = 0;
            ArkaniumCount = 0;
        }

        public IEnumerator RestartLevel()
        {
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}