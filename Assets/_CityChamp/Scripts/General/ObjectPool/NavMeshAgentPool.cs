// Set up the basic functionality for the nav mesh agent pool class

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SpectraStudios.CityChamp
{
    public class NavMeshAgentPool
    {
        private PooledNavMeshAgent _prefab;
        private int _size;
        private List<PooledNavMeshAgent> _availableNavMeshAgentsPool;

        private NavMeshAgentPool(PooledNavMeshAgent prefab, int size)
        {
            this._prefab = prefab;
            this._size = size;
            _availableNavMeshAgentsPool = new List<PooledNavMeshAgent>(size);
        }

        public static NavMeshAgentPool CreateInstance(PooledNavMeshAgent prefab, int size)
        {
            NavMeshAgentPool pool = new NavMeshAgentPool(prefab, size);

            GameObject poolGameObject = new GameObject(prefab + " Pool");
            pool.CreateObjects(poolGameObject);

            return pool;
        }

        private void CreateObjects(GameObject parent)
        {
            for (int i = 0; i < _size; i++)
            {
                // NavMeshAgents must be instantiated on the NavMesh, a crucial difference from other gameObjects that can be instantiated at Vector3.zero in the ObjectPool class
                PooledNavMeshAgent pooledNavMeshAgent = GameObject.Instantiate(_prefab, GetPositionOnNavMesh(), Quaternion.identity, parent.transform);

                pooledNavMeshAgent.Parent = this;
                pooledNavMeshAgent.gameObject.SetActive(false);
            }
        }

        public void ReturnObjectToPool(PooledNavMeshAgent Object)
        {
            _availableNavMeshAgentsPool.Add(Object);
        }

        public PooledNavMeshAgent GetObject()
        {
            if (_availableNavMeshAgentsPool.Count > 0)
            {
                PooledNavMeshAgent instance = _availableNavMeshAgentsPool[0];

                _availableNavMeshAgentsPool.RemoveAt(0);

                instance.gameObject.SetActive(true);

                return instance;
            }
            else
            {
                return null;
            }
        }

        private Vector3 GetPositionOnNavMesh()
        {
            for (int i = 0; i < 30; i++)
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(Vector3.zero, out hit, 100f, 1))
                {
                    return hit.position;
                }
            }
            return Vector3.zero;
        }
    }
}
