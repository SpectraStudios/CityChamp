// Edited on 6 March 2024

using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;
using SpectraStudios.CityChamp.Enemies;

namespace SpectraStudios.CityChamp
{
    [TaskDescription("Gets the object's health. Returns success if get.")]
    [TaskCategory("CityChamp")]
    public class GetSelfEnemyHealth : Action
    {
        [Tooltip("The shared health value to update")]
        public SharedInt SelfHealth;

        public override TaskStatus OnUpdate()
        {
            SelfHealth.Value = gameObject.GetComponent<Enemy>().Health;

            if (SelfHealth == null)
            {
                Debug.LogWarning("Unable to get health - SelfHealth is null");
                return TaskStatus.Failure;
            }
            return TaskStatus.Success;
        }
    }
}