using System;
using UnityEngine;
//using SpectraStudios.CityChamp.GameStates;
//using SpectraStudios.Citychamp.Reforms;

namespace SpectraStudios.CityChamp
{
    public class CityCore : MonoBehaviour, IDamageable
    {
        public static event Action<int> OnHealthChanged;
        public static event Action<int> OnMaxHealthIncreased;
        public static event Action OnDied;

        private int _maxHealth = 100;

        public int Health { get; set; }

        private void Awake()
        {
            //CombatState.OnWaveEnded += SetToMaxHealth;
            //CombatState.OnWaveFailed += SetToMaxHealth;
            //Reform.OnReformPassedIncreaseHealth += IncreaseMaxHealth;

            Health = _maxHealth;
        }

        private void OnDestroy()
        {
            //CombatState.OnWaveEnded -= SetToMaxHealth;
            //CombatState.OnWaveFailed -= SetToMaxHealth;
            //Reform.OnReformPassedIncreaseHealth -= IncreaseMaxHealth;
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

        public void TakeDamage(int damageAmount)
        {
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

        public void Death()
        {
            // Reset health to 0 in case the city got hit at the end
            Health = 0;
            OnHealthChanged?.Invoke(Health);

            // The wave is failed
            OnDied?.Invoke();

            Debug.LogWarning("City died!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }
}