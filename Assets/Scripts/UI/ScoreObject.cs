using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ScoreSpace.UI
{
    public class ScoreObject : UnityEngine.MonoBehaviour
    {
        [SerializeField] private Text _scoreText;
        [SerializeField] private float _disableDelay = 1f;
        
        private int _scoreValue;
        public int ScoreValue
        {
            get => _scoreValue;
            set
            {
                _scoreValue = value;
                _scoreText.text = _scoreValue.ToString();
                SetScale();
            }
        }

        private void SetScale()
        {
            Debug.Log(ScoreValue);
            if (ScoreValue <= 10)
            {
                gameObject.transform.localScale = new Vector3(1,1,1);
            }
            else if (ScoreValue <= 30)
            {
                transform.localScale = new Vector3(1.2f,1.2f,1.2f);
            }
            else if (ScoreValue <= 50)
            {
                transform.localScale = new Vector3(1.4f,1.4f,1.4f);
            }
            else if (ScoreValue <= 75)
            {
                transform.localScale = new Vector3(1.6f,1.6f,1.6f);
            }
            else if (ScoreValue <= 150)
            {
                transform.localScale = new Vector3(1.8f,1.8f,1.8f);
            }
            else
            {
                transform.localScale = new Vector3(2f,2f,2f);
            }

        }

        private void OnEnable()
        {
            StartCoroutine(DisableDelayed());
        }

        private IEnumerator DisableDelayed()
        {
            yield return new WaitForSeconds(_disableDelay);
            gameObject.SetActive(false);
            ScoreValue = 0;
        }
        
    }
}