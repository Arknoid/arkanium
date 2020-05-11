
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ScoreSpace
{
    public class MainMenu : UnityEngine.MonoBehaviour
    {
        [SerializeField] private GameObject _creditsPanel;
        [SerializeField] private Text _hiScoreText;
        
        private void Update()
        {
            if( Input.GetButton("Jump"))
            {
                StartGame();
            }
        }

        private void Start()
        {
            _creditsPanel.SetActive(false);
            _hiScoreText.text = "HiScore : " + PlayerPrefs.GetInt("HiScore", 0);
        }
        
        public void ShowHallOfFamePanel(bool show)
        {
        }
        public void ShowCreditsPanel(bool show)
        {
            _creditsPanel.SetActive(show);
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Scenes/Level1");
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