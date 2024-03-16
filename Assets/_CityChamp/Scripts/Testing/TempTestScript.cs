using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpectraStudios.CityChamp.Enemies;

namespace SpectraStudios.CityChamp
{
    public class TempTestScript : MonoBehaviour
    {

        public EnemySpawner EnemySpawnerScript;
        [SerializeField] private float initialSpawnSecs = 3f;
        [SerializeField] private float repeatSpawnSecs = 4f;

        private void Start()
        {
            InvokeRepeating("SpawnAnEnemy", initialSpawnSecs, repeatSpawnSecs);
        }

        private void SpawnAnEnemy()
        {
            int enemy = Random.Range(0, 2);

            switch (enemy)
            {
                case 0:
                    EnemySpawnerScript.SpawnEnemy(EnemyEnum.EnemyType.Fume);
                    break;

                case 1:
                    EnemySpawnerScript.SpawnEnemy(EnemyEnum.EnemyType.Nimbo);
                    break;
            }
        }    
    }
}