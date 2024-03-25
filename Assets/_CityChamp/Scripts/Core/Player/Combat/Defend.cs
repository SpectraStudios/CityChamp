using UnityEngine;
using System;
using System.Collections;

namespace SpectraStudios.CityChamp
{
    public class Defend : MonoBehaviour
    {
        public static event Action<bool> OnPlayerDefending;

        public OVRHand Hand;
        public Vector3 Offset;
        public GameObject Shield;

        [SerializeField] private bool _canShield = true;
        private float _shieldTime = 3f;
        private float _cooldownTime = 4f;

        private float _smoothSpeed = 0.125f;
        private Vector3 _velocity = Vector3.zero;

        private void LateUpdate()
        {
            if (Hand != null)
            {
                Shield.transform.position = Vector3.SmoothDamp(Shield.transform.position, Hand.transform.position + Offset, ref _velocity, _smoothSpeed * Time.deltaTime);
                Shield.transform.rotation = Quaternion.Euler(Vector3.SmoothDamp(Shield.transform.rotation.eulerAngles, Hand.transform.eulerAngles, ref _velocity, _smoothSpeed * Time.deltaTime));
            }
        }

        public void ActivateShield()
        {
            if (_canShield && !Shield.activeSelf)
            {
                Shield.SetActive(true);
                OnPlayerDefending?.Invoke(true);
            }

            _canShield = false;
            Invoke("DeactivateShield", _shieldTime);
            StartCoroutine(Cooldown());
        }

        // Shield is deactivated either when the player changes hand gesture or after they have been defending for a certain amount of time
        public void DeactivateShield()
        {
            if (Shield.activeSelf)
            {
                Shield.SetActive(false);
                OnPlayerDefending?.Invoke(false);
            }

            // Stop the invoke to deactivate if the player has already changed hand gestures
            if (IsInvoking("DeactivateShield"))
            {
                CancelInvoke("DeactivateShield");
            }
        }

        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(_cooldownTime);

            _canShield = true;
        }
    }
}
