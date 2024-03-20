// This script has the object follow its target. For example, an enemyHealthBar follows the enemy

using UnityEngine;

namespace SpectraStudios.CityChamp
{
    public class FollowPosition : MonoBehaviour
    {
        public Transform Target;
        public Vector3 Offset;

        private float _smoothSpeed = 0.125f;
        private Vector3 _velocity = Vector3.zero;

        private void LateUpdate()
        {
            if (Target != null)
            {
                transform.position = Vector3.SmoothDamp(transform.position, Target.position + Offset, ref _velocity, _smoothSpeed * Time.deltaTime);
            }
        }

        public void SetTarget(Transform newTarget)
        {
            Target = newTarget;
        }

        public void SetOffset(Vector3 newOffset)
        {
            Offset = newOffset;
        }
    }
}