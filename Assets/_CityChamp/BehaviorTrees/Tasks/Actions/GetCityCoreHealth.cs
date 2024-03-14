using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

namespace SpectraStudios.CityChamp
{
    [TaskDescription("Gets the CityCore's health. Returns success if get.")]
    [TaskCategory("CityChamp")]
    public class GetCityCoreHealth : Action
    {
        [Tooltip("The shared health value to update")]
        public SharedInt CityCoreHealth;

        public override TaskStatus OnUpdate()
        {
            CityCoreHealth.Value = GameObject.FindGameObjectWithTag("CityCore").GetComponent<CityCore>().Health;

            if (CityCoreHealth == null)
            {
                Debug.LogWarning("Unable to get health - CityCoreHealth is null");
                return TaskStatus.Failure;
            }
            return TaskStatus.Success;
        }
    }
}