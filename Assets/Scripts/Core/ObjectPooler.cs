using System.Collections.Generic;
using ScoreSpace.Patterns;
using UnityEngine;

namespace ScoreSpace.Core
{
    
    [System.Serializable]
    public class ObjectPoolItem {
        public int amountToPool;
        public GameObject objectToPool;
        public bool shouldExpand;
    }
    
    
    public class ObjectPooler : MonoSingleton<ObjectPooler>
    {
        private List<GameObject> _pooledObjects;
        public List<ObjectPoolItem> itemsToPool;
        
        private void Start () {
            _pooledObjects = new List<GameObject>();
            foreach (var item in itemsToPool) {
                for (var i = 0; i < item.amountToPool; i++) {
                    var obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    _pooledObjects.Add(obj);
                }
            }
        }
        
        public GameObject GetPooledObject(string objectTag)
        {
            if (objectTag == null) return null;
            foreach (var pooledObject in _pooledObjects)
            {
                if (!pooledObject.activeInHierarchy && pooledObject.CompareTag(objectTag)) {
                    return pooledObject;
                }
            }
            foreach (var item in itemsToPool)
            {
                if (!item.objectToPool.CompareTag(objectTag)) continue;
                if (!item.shouldExpand) continue;
                var obj = (GameObject)Instantiate(item.objectToPool,transform.position, Quaternion.identity);
                obj.SetActive(false);
                _pooledObjects.Add(obj);
                return obj;
            }
            return null;
        }
    }
}