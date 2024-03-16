using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

namespace SpectraStudios.CityChamp
{
    [TaskDescription("Sets the new target. Returns success if the target was set.")]
    [TaskCategory("CityChamp")]
    [TaskIcon("{SkinColor}ReflectionIcon.png")]
    public class SetTarget : Action
    {
        [Tooltip("The new target to set")]
        public string targetName;
        [Tooltip("The current target")]
        public SharedGameObject Target;

        public override TaskStatus OnUpdate()
	    {
            if (targetName == null)
            {
                Debug.LogWarning("Unable to set target - target name is null");
                return TaskStatus.Failure;
            }
            if (Target == null)
            {
                Debug.LogWarning("Unable to set target = Target is null");
                return TaskStatus.Failure;
            }

            Target.Value = GameObject.Find(targetName);

            return TaskStatus.Success;
        }
    }
}