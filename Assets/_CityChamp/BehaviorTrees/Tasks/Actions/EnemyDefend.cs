using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;
using SpectraStudios.CityChamp.Enemies;

namespace SpectraStudios.CityChamp
{
    [TaskDescription("Toggles whether the enemy is defending. Always returns success.")]
    [TaskCategory("CityChamp")]
    public class EnemyDefend : Action
    {
        [Tooltip("Whether the enemy should be defending")]
        public bool shouldDefend;

        public override TaskStatus OnUpdate()
        {
            if (shouldDefend)
            {
                gameObject.transform.GetComponent<Enemy>().IsDefending = true;
            }
            else
            {
                gameObject.transform.GetComponent<Enemy>().IsDefending = false;
            }
            return TaskStatus.Success;
        }
    }
}