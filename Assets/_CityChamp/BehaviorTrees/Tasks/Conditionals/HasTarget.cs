using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

namespace SpectraStudios.CityChamp
{
	[TaskDescription("Returns success if target is not null.")]
	[TaskCategory("CityChamp")]
	public class HasTarget : Conditional
	{
        [Tooltip("The current target")]
        public SharedGameObject Target;

		public override TaskStatus OnUpdate()
		{
			if (Target == null)
			{
                Debug.LogWarning("Unable to check target - Target is null");
                return TaskStatus.Failure;
			}

			if (Target.Value != null)
			{
				return TaskStatus.Success;
			}

			return TaskStatus.Failure;
		}
	}
}