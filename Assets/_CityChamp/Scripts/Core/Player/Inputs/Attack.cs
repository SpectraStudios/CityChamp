using UnityEngine;
using System.Collections;
using Oculus.Interaction;

namespace SpectraStudios.CityChamp
{
    public class Attack : MonoBehaviour
    {
        private bool _hasPinched;
        private bool _isIndexFingerPinching;
        private float _pinchStrength;
        private OVRHand.TrackingConfidence _confidence;

        public OVRHand Hand;
        public OVRHand.HandFinger Finger;
        public HandPointerPose PointerPose;

        private Vector3 _destination;
        private float _projectileSpeed = 8;
        private float _impactForce = 20;
        private int _damage = 40;

        [SerializeField] private bool _canBlast = true;
        private float _cooldownTime = 0.5f;

        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private int _numProjectiles = 5;
        private ObjectPool _projectilePool;

        private PooledObject _instance;

        private void Awake()
        {
            _projectilePool = ObjectPool.CreateInstance(_projectilePrefab, _numProjectiles);
        }

        private void Start()
        {
            Finger = OVRHand.HandFinger.Index;
        }

        private void Update()
        {
            CheckPinch(Hand, Finger);
        }

        private void CheckPinch(OVRHand hand, OVRHand.HandFinger finger)
        {
            _pinchStrength = hand.GetFingerPinchStrength(finger);
            _isIndexFingerPinching = hand.GetFingerIsPinching(finger);
            _confidence = hand.GetFingerConfidence(finger);

            if (!_hasPinched && _isIndexFingerPinching && _confidence == OVRHand.TrackingConfidence.High)
            {
                _hasPinched = true;
                Blast();
            }
            else if (_hasPinched && !_isIndexFingerPinching)
            {
                _hasPinched = false;
            }
        }


        public void Blast()
        {
            if (_canBlast)
            {
                Debug.LogWarning("shoooooting");

                RaycastHit hit;
                if (Physics.Raycast(PointerPose.transform.position, transform.forward, out hit))
                {
                    Debug.LogWarning("hiiiiiiiiiiiiiiiiiiiiiiiiit");
                    _destination = hit.point;

                    IDamageable damageable = hit.transform.GetComponent<IDamageable>();

                    if (damageable == null)
                    {
                        damageable = hit.transform.GetComponentInParent<IDamageable>();
                    }

                    if (damageable == null)
                    {
                        damageable = hit.transform.GetComponentInChildren<IDamageable>();
                    }
                    Debug.LogWarning(damageable);

                    if (damageable != null)
                    {
                        Debug.LogWarning("damageableeeeeeeeeeeeeeeee");

                        if (hit.transform.tag != "Player" && hit.transform.tag != "CityCore")
                        {
                            if (hit.rigidbody != null)
                            {
                                hit.rigidbody.AddForce(-hit.normal * _impactForce);
                            }

                            damageable.TakeDamage(_damage);
                        }
                    }
                }
                else
                {
                    Ray r = new Ray(PointerPose.transform.position, Vector3.forward + Vector3.up);
                    _destination = r.GetPoint(100);
                }

                _instance = _projectilePool.GetObject();

                if (_instance != null)
                {
                    _instance.transform.SetPositionAndRotation(PointerPose.transform.position, PointerPose.transform.rotation);
                    _instance.GetComponent<Rigidbody>().velocity = (_destination - PointerPose.transform.position).normalized * _projectileSpeed;
                }

                _canBlast = false;
                StartCoroutine(Cooldown());
            }
        }

        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(_cooldownTime);

            _canBlast = true;
        }
    }
}
