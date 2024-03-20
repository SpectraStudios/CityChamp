// This script has the object follow its target. For example, the player uses the position and rotation data of the main camera

using UnityEngine;

namespace SpectraStudios.CityChamp
{
    public class FollowPositionAndRotation : MonoBehaviour
    {
        public Transform Target;
        public Vector3 Offset;

        private float _smoothSpeed = 0.025f;
        private Vector3 _velocity = Vector3.zero;

        private void LateUpdate()
        {
            if (Target != null)
            {
                transform.rotation = Quaternion.Euler(Vector3.SmoothDamp(transform.rotation.eulerAngles, Target.eulerAngles, ref _velocity, _smoothSpeed * Time.deltaTime));
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