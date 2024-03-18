using UnityEngine;

namespace SpectraStudios.CityChamp
{
    public class AutoDestroyPooledObject : PooledObject
    {
        public virtual float AutoDestroyTime { get { return 5f; } }

        protected const string DisableMethodName = "Disable";

        public virtual void OnEnable()
        {
            // Cancel any previous invoke so that all the pooled objects are not disabled at the same time
            CancelInvoke(DisableMethodName);

            // Start a new invoke for each pooled object when it is enabled
            Invoke(DisableMethodName, AutoDestroyTime);
        }

        // Disable the game object, which will then have PooledObject.cs return it to the pool
        public virtual void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
