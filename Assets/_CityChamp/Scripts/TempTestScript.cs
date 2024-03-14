using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpectraStudios.CityChamp.Enemies;

namespace SpectraStudios.CityChamp
{
    public class TempTestScript : MonoBehaviour
    {

        public EnemySpawner EnemySpawnerScript;

        private void Start()
        {
            InvokeRepeating("SpawnAnEnemy", 3f, 3f);
        }

        private void SpawnAnEnemy()
        {
            int enemy = Random.Range(0, 1);

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