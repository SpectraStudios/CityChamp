using UnityEngine;
using System.Collections;
using Oculus.Interaction;

namespace SpectraStudios.CityChamp
{
    public class Attack : MonoBehaviour
    {
        private bool _hasPinched;
        private bool _isIndexFingerPinching;
        private float _indexPinchStrength;
        private OVRHand.TrackingConfidence _indexConfidence;

        private bool _isMiddleFingerPinching;
        private float _middlePinchStrength;
        private OVRHand.TrackingConfidence _middleConfidence;

        public OVRHand Hand;
        public OVRHand.HandFinger IndexFinger;
        public OVRHand.HandFinger MiddleFinger;

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
            IndexFinger = OVRHand.HandFinger.Index;
            MiddleFinger = OVRHand.HandFinger.Middle;
        }

        private void Update()
        {
            CheckPinch(Hand, IndexFinger, MiddleFinger);
        }

        private void CheckPinch(OVRHand hand, OVRHand.HandFinger indexFinger, OVRHand.HandFinger middleFinger)
        {
            _indexPinchStrength = hand.GetFingerPinchStrength(indexFinger);
            _isIndexFingerPinching = hand.GetFingerIsPinching(indexFinger);
            _indexConfidence = hand.GetFingerConfidence(indexFinger);

            _middlePinchStrength = hand.GetFingerPinchStrength(middleFinger);
            _isMiddleFingerPinching = hand.GetFingerIsPinching(middleFinger);
            _middleConfidence = hand.GetFingerConfidence(middleFinger);


            if (!_hasPinched && _isIndexFingerPinching && _isMiddleFingerPinching && _indexConfidence == OVRHand.TrackingConfidence.High && _middleConfidence == OVRHand.TrackingConfidence.High)
            {
                _hasPinched = true;
                Blast();
            }
            else if (_hasPinched && !_isIndexFingerPinching && !_isMiddleFingerPinching)
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
