using UnityEngine;
using UnityEngine.AI;

namespace SpectraStudios.CityChamp.Enemies
{
    [CreateAssetMenu(fileName = "Enemy Configuration", menuName = "ScriptableObject/Enemy Configuration")]
    public class EnemyScriptableObject : ScriptableObject
    {
        // Enemy Stats
        public int Health = 100;
        public int ReformPoints = 500;
        public float DeathAnimTime = 4.0f;

        // NavMeshAgent Configs
        public float Speed = 1f;
        public float AngularSpeed = 120f;
        public float Acceleration = 4f;
        public float StoppingDistance = 1f;
        public float BaseOffset = 0f;
        public float Radius = 0.5f;
        public float Height = 2f;
        public int AvoidancePriority = 50;
        public int AreaMask = 1;
        public ObstacleAvoidanceType ObstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    }
}