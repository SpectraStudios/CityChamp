// Edited on 6 March 2024

using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

namespace SpectraStudios.CityChamp
{
    [TaskDescription("A short-range forward melee attack. Returns success if the attack is completed, regardless of whether it hits something.")]
    [TaskCategory("CityChamp")]
    public class EnemyMeleeAttack : Action
    {
        [Tooltip("The target to attack")]
        public SharedGameObject Target;
        [Tooltip("The damage dealt by the attack")]
        public int Damage;

        public override TaskStatus OnUpdate()
        {
            if (Target == null)
            {
                Debug.LogWarning("Unable to attack target - Target is null");
                return TaskStatus.Failure;
            }

            if (Damage <= 0)
            {
                Debug.LogWarning("Unable to attack target - Damage is 0");
                return TaskStatus.Failure;
            }

            Target.Value.transform.GetComponent<IDamageable>().TakeDamage(Damage);

            return TaskStatus.Success;
        }
    }
}