// Edited on 7 March 2024

using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

namespace SpectraStudios.CityChamp
{
    [TaskDescription("Returns success if nearby gameObject has target tag.")]
    [TaskCategory("CityChamp")]
    public class CheckPlayerNearby : Action
    {
        [Tooltip("The target tag to check for")]
        public string targetTag;
        [Tooltip("The distance that the target needs to be within")]
        public float magnitude = 5;
        [Tooltip("The current target")]
        public SharedGameObject Target;

        public override TaskStatus OnUpdate()
        {
            if (string.IsNullOrEmpty(targetTag))
            {
                Debug.LogWarning("Unable to check for target - targetTag is null");
                return TaskStatus.Failure;
            }

            if (Target == null)
            {
                Debug.LogWarning("Unable to compare target - Target is null");
                return TaskStatus.Failure;
            }

            var objects = GameObject.FindGameObjectsWithTag(targetTag);
            for (int i = 0; i < objects.Length; ++i)
            {
                if (IsWithinDistance(objects[i]))
                {
                    Target.Value = objects[i];
                    return TaskStatus.Success;
                }
            }
            return TaskStatus.Failure;
        }

        private bool IsWithinDistance(GameObject target)
        {
            var direction = target.transform.position - gameObject.transform.position;
            // check to see if the square magnitude is less than what is specified
            if (Vector3.SqrMagnitude(direction) < magnitude)
            {
                return true;
            }
            return false;
        }
    }
}
