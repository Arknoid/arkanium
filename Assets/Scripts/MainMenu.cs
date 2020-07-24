
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
        [SerializeField] private Text _versionText;
        
        private void Update()
        {
            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Cancel"))
            {
                StartGame();
            }
        }

        private void Start()
        {
            _creditsPanel.SetActive(false);
            _hiScoreText.text = "HiScore : " + PlayerPrefs.GetInt("HiScore", 0);
            _versionText.text = "Version : " + Application.version;
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