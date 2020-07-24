using ScoreSpace.Patterns;
using UnityEngine;

namespace ScoreSpace.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        private int _playerScore;
        public bool NewHiScore { get; private set; } = false;
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

        protected override void Init()
        {
            GameObject.DontDestroyOnLoad(this);
            base.Init();
        }
    }
}