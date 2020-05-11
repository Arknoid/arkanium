using UnityEngine;

namespace ScoreSpace.Player
{
    public class PlayerInput : MonoBehaviour
    {

        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        public bool IsFireHolding { get; private set; }
        public bool IsFullFireHolding { get; private set; }
        
        [SerializeField] private float _holdFirePressThreshold = 0.5f;
        [SerializeField] private float _holdFireFullPressThreshold = 2f;
        
        private bool _isFire;
        private bool _isHoldFireButton;
        private float _holdFireTimer = 0f;
        

        public delegate void OnFireHandler();
        public event OnFireHandler OnFire;
        public delegate void OnHoldFireHandler(bool isFull);
        public event OnHoldFireHandler OnHoldFire;

        private void Update()
        {
            CheckInput();
            if (_isHoldFireButton)
            {
                IncrementHoldFireButtonTimer();
            }

            if (!_isFire) return;
            if (_holdFireTimer <= _holdFirePressThreshold)
            {
                OnFire?.Invoke();
                ResetHoldFireButtonTimer();
            }
            else
            {
                OnHoldFire?.Invoke(_holdFireTimer >= _holdFireFullPressThreshold);
                ResetHoldFireButtonTimer();
            }
        }
        
        private void CheckInput()
        {
            IsFireHolding = _holdFireTimer >= _holdFirePressThreshold;
            IsFullFireHolding = _holdFireTimer >= _holdFireFullPressThreshold;
            
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            _isFire = Input.GetButtonUp("Fire1") || Input.GetButtonUp("Jump");
            _isHoldFireButton = Input.GetButton("Fire1") || Input.GetButton("Jump");

        }

        private void OnDisable()
        {
            IsFireHolding = false;
        }

        private void OnDestroy()
        {
            IsFireHolding = false;
        }

        private void IncrementHoldFireButtonTimer()
        {
            _holdFireTimer += Time.deltaTime;
        }
        
        private void ResetHoldFireButtonTimer()
        {
            _holdFireTimer = 0;
        }
    }
}
