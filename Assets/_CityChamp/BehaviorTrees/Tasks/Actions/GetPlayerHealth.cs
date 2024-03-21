using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

namespace SpectraStudios.CityChamp
{
    [TaskDescription("Gets the player's health. Returns success if get.")]
    [TaskCategory("CityChamp")]
    public class GetPlayerHealth : Action
    {
        [Tooltip("The shared health value to update")]
        public SharedInt PlayerHealth;

        public override TaskStatus OnUpdate()
        {
            PlayerHealth.Value = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().Health;

            if (PlayerHealth == null)
            {
                Debug.LogWarning("Unable to get health - PlayerHealth is null");
                return TaskStatus.Failure;
            }
            return TaskStatus.Success;
        }
    }
}