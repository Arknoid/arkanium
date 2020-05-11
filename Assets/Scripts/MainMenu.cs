
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScoreSpace
{
    public class MainMenu : UnityEngine.MonoBehaviour
    {
        [SerializeField] private GameObject _hallOfFamePanel;
        [SerializeField] private GameObject _creditsPanel;


        private void Start()
        {
            _hallOfFamePanel.SetActive(false);
            _creditsPanel.SetActive(false);
        }
        
        public void ShowHallOfFamePanel(bool show)
        {
            _hallOfFamePanel.SetActive(show);
        }
        public void ShowCreditsPanel(bool show)
        {
            _hallOfFamePanel.SetActive(show);
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Scenes/Level1");
        }
        
        
    }
}