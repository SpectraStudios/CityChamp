// Manages player stats, including health and how many times the player has been hit in a wave

using System;
using UnityEngine;

namespace SpectraStudios.CityChamp
{
    public class PlayerCombat : MonoBehaviour, IDamageable
    {
        public static event Action<int> OnHealthChanged;
        public static event Action<int> OnMaxHealthIncreased;
        public static event Action OnDied;

        [HideInInspector] public int PlayerHitCounter = 0;
        [HideInInspector] public bool IsDefending = false;
        [HideInInspector] public bool IsDead { get; private set; }

        private int _maxHealth = 100;

        public int Health { get; set; }

        private void Awake()
        {
            IsDead = false;
            Health = _maxHealth;

            Defend.OnPlayerDefending += SetIsDefending;
        }

        private void OnDestroy()
        {
            Defend.OnPlayerDefending -= SetIsDefending;
        }

        private void Start()
        {
            SetToMaxHealth();
        }

        public void SetToMaxHealth()
        {
            Health = _maxHealth;
            OnHealthChanged?.Invoke(Health);
        }

        public void IncreaseMaxHealth(int amount)
        {
            _maxHealth += amount;
            OnMaxHealthIncreased?.Invoke(_maxHealth);

            // Increasing max health adds that amount to current health too
            Health += amount;
            OnHealthChanged?.Invoke(Health);
        }

        public void ResetPlayerHitCounter()
        {
            PlayerHitCounter = 0;
        }

        public void IncreasePlayerHitCounter()
        {
            PlayerHitCounter += 1;
        }

        private void SetIsDefending(bool isDefending)
        {
            IsDefending = isDefending;
        }

        public void TakeDamage(int damageAmount)
        {
            if (!IsDead && !IsDefending)
            {
                IncreasePlayerHitCounter();

                Health -= damageAmount;

                if (Health <= 0)
                {
                    Death();
                }
                else
                {
                    OnHealthChanged?.Invoke(Health);
                }
            }
        }

        public void Death()
        {
            IsDead = true;

            // Reset health to 0 in case the player got hit at the end
            Health = 0;
            OnHealthChanged?.Invoke(Health);

            // Trigger respawn
            OnDied?.Invoke();
        }
    }
}
