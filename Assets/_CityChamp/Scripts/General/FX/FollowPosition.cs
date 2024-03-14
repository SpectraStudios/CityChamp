// This script has the object follow its target. For example, an enemyHealthBar follows the enemy

using UnityEngine;

namespace SpectraStudios.CityChamp
{
    public class FollowPosition : MonoBehaviour
    {
        public Transform Target; // assigned in inspector or by script
        public Vector3 Offset; // assigned in inspector or by script

        private float _smoothSpeed = 0.125f;
        private Vector3 _velocity = Vector3.zero;

        void LateUpdate()
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