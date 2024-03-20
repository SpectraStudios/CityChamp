// This script has the object move to the position of the target at runtime. For example, the city core canvas

using UnityEngine;

namespace SpectraStudios.CityChamp
{
    public class CopyPosition : MonoBehaviour
    {
        public Transform Target;
        public Vector3 Offset;

        private void Start()
        {
            transform.position = Target.position + Offset;
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