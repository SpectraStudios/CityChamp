using UnityEngine;

namespace SpectraStudios.CityChamp
{
    public class Projectile : AutoDestroyPooledObject
    {
        public override float AutoDestroyTime { get { return 1f; } }

        private int _damage = 40;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag != "Player" && collision.transform.tag != "CityCore")
            {
                TryDamageable(collision.transform);
            }

            Disable();
        }

        private void TryDamageable(Transform hitObject)
        {
            IDamageable damageable = hitObject.GetComponent<IDamageable>();
            if (damageable == null) { damageable = hitObject.GetComponentInParent<IDamageable>(); }
            if (damageable == null) { damageable = hitObject.GetComponentInChildren<IDamageable>(); }
            if (damageable != null) { Damage(damageable); }
        }

        private void Damage(IDamageable damageable)
        {
            damageable.TakeDamage(_damage);
        }
    }
}
