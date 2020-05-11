using System.Collections;
using ScoreSpace.Core;
using ScoreSpace.Managers;
using UnityEngine;


namespace ScoreSpace.Player
{
    public class PlayerHealth : Health
    {
        private Rigidbody2D _rb;
        protected override void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            base.Awake();
        }


        
        protected override IEnumerator Explode()
        {
            if (_rb!= null)_rb.simulated = false;
            GameManager.Instance.Loose();
            yield return StartCoroutine(base.Explode());
        }
    }
}