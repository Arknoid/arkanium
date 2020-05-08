using UnityEngine;
using UnityEngine.UI;

namespace ScoreSpace.UI
{
    public class SliderBar : UnityEngine.MonoBehaviour
    {
        [SerializeField] private Gradient _gradient;
        private Slider _slider;
        [SerializeField] 
        private Image _border;
        [SerializeField]
        private Image _fill;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }


        public void SetMaxValue(int value)
        {
            
            _slider.maxValue = value;
            _slider.value = value;
            // _border.color = _gradient.Evaluate(1f);
            _fill.color = _gradient.Evaluate(1f);
        }  

        public void SetValue(int health)
        {
            _slider.value = health;

            _border.color = _gradient.Evaluate(_slider.normalizedValue);
            
            _fill.color = _gradient.Evaluate(_slider.normalizedValue);
        }
        
    }
}