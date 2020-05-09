using System;
using ScoreSpace.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScoreSpace
{
    public class LootOnTrigger : UnityEngine.MonoBehaviour
    {
        [SerializeField] private string _tagItemTOLoot;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            const float range = 2f;
            var randomPos = Random.insideUnitSphere * range;
            var spawnedLoot = ObjectPooler.Instance.GetPooledObject(_tagItemTOLoot);
            if (spawnedLoot == null) return;
            spawnedLoot.transform.position = transform.position + randomPos;
            spawnedLoot.SetActive(true);
        }
    }
}