using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScoreSpace.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace ScoreSpace.Core
{
    [Serializable]
    public class ItemToLoot
    {
        [FormerlySerializedAs("Tag")] [TagSelector]
        public string _tag;
        [FormerlySerializedAs("ChanceToLoot")] [Range(0,100)]
        public float _chanceToLoot;
    }
    public class EnemyHealth : Health
    {

        [SerializeField] private bool _lootOnDie = true;
        [SerializeField] private ItemToLoot[] _itemsToLoots;
        
        
        
        
        
        protected override IEnumerator Explode()
        {
            if (_lootOnDie)
            {
                var randomNumber = Random.Range(0f, 100f);
                var lootsTag = new List<string>();  
                foreach (var item in _itemsToLoots)
                {
                    if ( randomNumber <= item._chanceToLoot)
                    {
                        lootsTag.Add(item._tag);
                    }
                }

                if (!lootsTag.Any()) return base.Explode();
                var spawnedLoot = ObjectPooler.Instance.GetPooledObject(lootsTag.Count > 1 ? lootsTag[Random.Range(0,lootsTag.Count )] : lootsTag[0]);
                if (spawnedLoot == null) return base.Explode();
                spawnedLoot.transform.position = transform.position;
                spawnedLoot.SetActive(true);

            }
            return base.Explode();
        }
    }
}