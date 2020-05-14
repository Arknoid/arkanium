
using ScoreSpace.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScoreSpace
{
    public class LootOnTrigger : UnityEngine.MonoBehaviour
    {
        [SerializeField] private string _tagItemTOLoot;
        [SerializeField] private float _lootRange = 4f;
        [SerializeField] private int _numberOfItems = 4;
        [SerializeField] private bool _isRandom = true;
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            int numberOfLoot = _numberOfItems;

            if (_isRandom)
            {
                numberOfLoot = Random.Range(0, _numberOfItems);
            }
            
            for (int i = 0; i < numberOfLoot; i++)
            {
                var randomPos = Random.insideUnitCircle * _lootRange;
                var spawnedLoot = ObjectPooler.Instance.GetPooledObject(_tagItemTOLoot);
                if (spawnedLoot == null) return;
                spawnedLoot.transform.position = transform.position + new Vector3(randomPos.x , randomPos.y, 24.47266f );
                spawnedLoot.SetActive(true);
            }

        }
    }
}