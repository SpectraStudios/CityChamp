// Manages player stats, including health and how many times the player has been hit in a wave

using System;
using UnityEngine;
//using SpectraStudios.CityChamp.GameStates;

namespace SpectraStudios.CityChamp
{
    public class Player : MonoBehaviour, IDamageable
    {
        public static event Action<int> OnPlayerHealthChanged;
        public static event Action<int> OnPlayerMaxHealthIncreased;
        public static event Action OnPlayerDied;

        [HideInInspector] public int PlayerHitCounter = 0;

        private int _maxHealth = 100;

        public int Health { get; set; }

        private void Awake()
        {
            //CombatState.OnWaveEnded += SetToMaxHealth;
            //CombatState.OnWaveFailed += SetToMaxHealth;
            //CombatState.OnWaveStarted += ResetPlayerHitCounter;

            Health = _maxHealth;
        }

        private void OnDestroy()
        {
            //CombatState.OnWaveEnded -= SetToMaxHealth;
            //CombatState.OnWaveFailed -= SetToMaxHealth;
            //CombatState.OnWaveStarted -= ResetPlayerHitCounter;
        }

        private void Start()
        {
            SetToMaxHealth();
        }

        public void SetToMaxHealth()
        {
            Health = _maxHealth;
            OnPlayerHealthChanged?.Invoke(Health);
        }

        public void IncreaseMaxHealth(int amount)
        {
            _maxHealth += amount;
            OnPlayerMaxHealthIncreased?.Invoke(_maxHealth);

            // Increasing max health adds that amount to current health too
            Health += amount;
            OnPlayerHealthChanged?.Invoke(Health);
        }

        public void ResetPlayerHitCounter()
        {
            PlayerHitCounter = 0;
        }

        public void IncreasePlayerHitCounter()
        {
            PlayerHitCounter += 1;
        }

        public void TakeDamage(int damageAmount)
        {
            IncreasePlayerHitCounter();

            Health -= damageAmount;

            if (Health <= 0)
            {
                Death();
            }
            else
            {
                OnPlayerHealthChanged?.Invoke(Health);
            }
        }

        public void Death()
        {
            // Reset health to 0 in case the player got hit at the end
            Health = 0;
            OnPlayerHealthChanged?.Invoke(Health);

            // Trigger respawn
            OnPlayerDied?.Invoke();
        }


    }
}
