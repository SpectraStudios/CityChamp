// Set up pools and spawn classes for each type of enemy

using UnityEngine;
using UnityEngine.AI;

namespace SpectraStudios.CityChamp.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Fume _fumePrefab;
        [SerializeField] private int _numFumes = 5;
        private NavMeshAgentPool _fumePool;

        [SerializeField] private Nimbo _nimboPrefab;
        [SerializeField] private int _numNimbos = 5;
        private NavMeshAgentPool _nimboPool;

        private PooledNavMeshAgent _instance;

        private NavMeshTriangulation triangulation;

        // Set up the enemy spawn pools
        private void Awake()
        {
            _fumePool = NavMeshAgentPool.CreateInstance(_fumePrefab, _numFumes);
            _nimboPool = NavMeshAgentPool.CreateInstance(_nimboPrefab, _numNimbos);

            triangulation = NavMesh.CalculateTriangulation();
        }

        public void SpawnEnemy(EnemyEnum.EnemyType enemyType)
        {
            // try to get an enemy of the requested type from the pool
            switch (enemyType)
            {
                case EnemyEnum.EnemyType.Fume:

                    _instance = _fumePool.GetObject();

                    if (_instance != null)
                    {
                        Fume fume = _instance.GetComponent<Fume>();

                        PositionEnemy(fume, GetRandomNavMeshPosition());
                    }

                    break;

                case EnemyEnum.EnemyType.Nimbo:

                    _instance = _nimboPool.GetObject();

                    if (_instance != null)
                    {
                        Nimbo nimbo = _instance.GetComponent<Nimbo>();

                        PositionEnemy(nimbo, GetRandomNavMeshPosition());
                    }

                    break;
            }
        }

        // By separating this into its own class, we could use several different methods of setting a spawnPosition
        private void PositionEnemy(Enemy enemy, Vector3 spawnPosition)
        {
            enemy.transform.position = spawnPosition;
        }

        private Vector3 GetRandomNavMeshPosition()
        {
            for (int i = 0; i < 30; i++)
            {
                int vertexIndex = Random.Range(0, triangulation.vertices.Length);
                NavMeshHit hit;
                if (NavMesh.SamplePosition(triangulation.vertices[vertexIndex], out hit, 2f, 1))
                {
                    return hit.position;
                }
            }
            return Vector3.zero;
        }
    }
}