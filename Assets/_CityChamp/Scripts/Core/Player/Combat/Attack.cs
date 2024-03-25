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

        [SerializeField] private Transform _start;
        private float _projectileSpeed = 12;
        

        [SerializeField] private bool _canBlast = true;
        private float _cooldownTime = 0.6f;

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
                _instance = _projectilePool.GetObject();

                if (_instance != null)
                {
                    _instance.transform.SetPositionAndRotation(_start.position, _start.rotation);
                    _instance.GetComponent<Rigidbody>().velocity = _start.transform.forward * _projectileSpeed;
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
