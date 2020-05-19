﻿
using System;
using System.Collections;
using Pathfinding;
using ScoreSpace.Interfaces;
using ScoreSpace.Player;
using UnityEngine;

namespace ScoreSpace
{
    public class TargetShot : MonoBehaviour, IProjectile
    {
        public int Power { get; set; }
        [SerializeField] private int _power = 10;
        [SerializeField] private float _speed = 2;
        [SerializeField] private string _targetTag = "Player";
        [SerializeField] private float _attackDelay = 0.5f;
        private AIDestinationSetter _aiDestinationSetter;
        
        private Rigidbody2D _rb;
        private bool _canDamage = true;
        
        private GameObject _target;
        private void Awake()
        {
            _aiDestinationSetter = GetComponent<AIDestinationSetter>();
            Power = _power;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _canDamage = true;
            _target = GameObject.FindGameObjectWithTag(_targetTag);
            if (_target == null)
            {
                this.gameObject.SetActive(false);
                return;
            }

            _aiDestinationSetter.target = _target.transform;
        }

        private void OnEnable()
        {
            _canDamage = true;
            _target = GameObject.FindGameObjectWithTag(_targetTag);
            if (_target == null)
            {
                this.gameObject.SetActive(false);
                return;
            }

            _aiDestinationSetter.target = _target.transform;
        }

        private void Update()
        {
            if (_target.gameObject.activeInHierarchy)
            {
                // _rb.velocity = (_target.transform.position -_rb.transform.position).normalized * _speed;
                Vector2 lookDir = _target.transform.position - transform.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90;
                _rb.rotation = angle;
            }
            else
            {
                GetComponent<AIPath>().canMove = false;
            }

        }
        

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_canDamage && other.transform.CompareTag("Player"))
            {
                other.gameObject.GetComponent<IDamageable>()?.TakeDamage(Power);
                _canDamage = false;
                StartCoroutine(ResetCanDamage());
            }
        }


        private IEnumerator ResetCanDamage()
        {
            yield return new WaitForSeconds(_attackDelay);
            _canDamage = true;
        }
        // private void OnTriggerEnter2D(Collider2D other)
        // {
        //     other.gameObject.GetComponent<IDamageable>()?.TakeDamage(Power);
        //     this.gameObject.SetActive(false);
        // }
    }
}