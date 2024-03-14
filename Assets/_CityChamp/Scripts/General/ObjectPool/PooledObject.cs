using UnityEngine;

namespace SpectraStudios.CityChamp
{
    public class PooledObject : MonoBehaviour
    {
        public ObjectPool Parent;

        // Return an object to its pool when it is disabled
        public virtual void OnDisable()
        {
            Parent.ReturnObjectToPool(this);
        }
    }
}