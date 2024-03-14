// Edited on 6 March 2024

using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

namespace SpectraStudios.CityChamp
{
    [TaskDescription("Returns success if target name matches the current target.")]
    [TaskCategory("CityChamp")]
    public class CompareTarget : Conditional
	{
        [Tooltip("The target tag to compare against")]
        public string targetTag;
        [Tooltip("The current target")]
        public SharedGameObject Target;

        public override TaskStatus OnUpdate()
        {
            if (targetTag == null)
            {
                Debug.LogWarning("Unable to compare target - targetTag is null");
                return TaskStatus.Failure;
            }

            if (Target == null)
            {
                Debug.LogWarning("Unable to compare target - Target is null");
                return TaskStatus.Failure;
            }

            if (Target.Value.tag == targetTag)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}
