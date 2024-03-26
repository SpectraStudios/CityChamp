using UnityEngine;
using System;
using System.Collections;

namespace SpectraStudios.CityChamp
{
    public class Defend : MonoBehaviour
    {
        public static event Action<bool> OnPlayerDefending;

        public OVRHand Hand;
        public GameObject Shield;

        [SerializeField] private bool _canShield = true;
        private float _shieldTime = 5f;
        private float _cooldownTime = 7f;

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
