using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

namespace SpectraStudios.CityChamp
{
    [TaskDescription("Sets the new object to animate. Returns success if set.")]
    [TaskCategory("CityChamp")]
    [TaskIcon("{SkinColor}ReflectionIcon.png")]
    public class SetAnimator : Action
    {
        [Tooltip("The object to animate")]
        public SharedGameObject Animator;

        public override TaskStatus OnUpdate()
        {
            Animator.Value = gameObject;

            if (Animator.Value == null)
            {
                Debug.LogWarning("Unable to set Animator - Animator is null");
                return TaskStatus.Failure;
            }

            return TaskStatus.Success;
        }
    }
}