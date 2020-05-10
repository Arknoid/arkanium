using System;
using ScoreSpace.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScoreSpace
{
    public class LootOnTrigger : UnityEngine.MonoBehaviour
    {
        [SerializeField] private string _tagItemTOLoot;
        [SerializeField] private float _lootRange = 4f;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var randomPos = Random.insideUnitSphere * _lootRange;
            var spawnedLoot = ObjectPooler.Instance.GetPooledObject(_tagItemTOLoot);
            if (spawnedLoot == null) return;
            spawnedLoot.transform.position = transform.position + randomPos;
            spawnedLoot.SetActive(true);
        }
    }
}