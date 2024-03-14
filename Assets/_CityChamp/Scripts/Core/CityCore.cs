using System;
using UnityEngine;
//using SpectraStudios.CityChamp.GameStates;
//using SpectraStudios.Citychamp.Reforms;

namespace SpectraStudios.CityChamp
{
    public class CityCore : MonoBehaviour, IDamageable
    {
        public static event Action<int> OnCityCoreHealthChanged;
        public static event Action<int> OnCityCoreMaxHealthIncreased;
        public static event Action OnCityCoreDied;

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
            OnCityCoreHealthChanged?.Invoke(Health);
        }

        public void IncreaseMaxHealth(int amount)
        {
            _maxHealth += amount;
            OnCityCoreMaxHealthIncreased?.Invoke(_maxHealth);

            // Increasing max health adds that amount to current health too
            Health += amount;
            OnCityCoreHealthChanged?.Invoke(Health);
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
                OnCityCoreHealthChanged?.Invoke(Health);
            }
        }

        public void Death()
        {
            // Reset health to 0 in case the city got hit at the end
            Health = 0;
            OnCityCoreHealthChanged?.Invoke(Health);

            // The wave is failed
            OnCityCoreDied?.Invoke();
        }
    }
}