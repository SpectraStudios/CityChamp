// Set up the basic functionality for the object pool class

using System.Collections.Generic;
using UnityEngine;

namespace SpectraStudios.CityChamp
{
    public class ObjectPool
    {
        private PooledObject _prefab;
        private int _size;
        private List<PooledObject> _availableObjectsPool;

        private ObjectPool(PooledObject prefab, int size)
        {
            this._prefab = prefab;
            this._size = size;
            _availableObjectsPool = new List<PooledObject>(size);
        }

        public static ObjectPool CreateInstance(PooledObject prefab, int size)
        {
            ObjectPool pool = new ObjectPool(prefab, size);

            GameObject poolGameObject = new GameObject(prefab + " Pool");
            pool.CreateObjects(poolGameObject);

            return pool;
        }

        private void CreateObjects(GameObject parent)
        {
            for (int i = 0; i < _size; i++)
            {
                PooledObject pooledObject = GameObject.Instantiate(_prefab, Vector3.zero, Quaternion.identity, parent.transform);
                pooledObject.Parent = this;
                pooledObject.gameObject.SetActive(false);
            }
        }

        public void ReturnObjectToPool(PooledObject Object)
        {
            _availableObjectsPool.Add(Object);
        }

        public PooledObject GetObject()
        {
            if (_availableObjectsPool.Count > 0)
            {
                PooledObject instance = _availableObjectsPool[0];

                _availableObjectsPool.RemoveAt(0);

                instance.gameObject.SetActive(true);

                return instance;
            }
            else
            {
                return null;
            }
        }
    }
}