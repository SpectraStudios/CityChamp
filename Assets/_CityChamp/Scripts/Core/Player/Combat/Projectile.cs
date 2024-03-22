using UnityEngine;

namespace SpectraStudios.CityChamp
{
    public class Projectile : AutoDestroyPooledObject
    {
        public override float AutoDestroyTime { get { return 3f; } }

        public GameObject Blast;
        public GameObject Impact;

        private int _damage = 40;

        public override void OnEnable()
        {
            base.OnEnable();
            
            Blast.SetActive(true);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Blast.SetActive(false);
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

            if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "CityCore")
            {
                TryDamageable(collision.transform);
            }
            else
            {
                Disable();
            }
        }

        private void TryDamageable(Transform hitObject)
        {
            IDamageable damageable = hitObject.GetComponent<IDamageable>();
            if (damageable == null) { damageable = hitObject.GetComponentInParent<IDamageable>(); }
            if (damageable == null) { damageable = hitObject.GetComponentInChildren<IDamageable>(); }

            if (damageable != null) { Damage(damageable); }
            else { Disable(); }
        }

        private void Damage(IDamageable damageable)
        {
            damageable.TakeDamage(_damage);

            Impact.SetActive(true);
        }

        public override void Disable()
        {
            Impact.SetActive(false);

            base.Disable();
        }
    }
}
