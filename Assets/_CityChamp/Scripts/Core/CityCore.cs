using System;
using UnityEngine;

namespace SpectraStudios.CityChamp
{
    public class CityCore : MonoBehaviour, IDamageable
    {
        public static event Action<int> OnHealthChanged;
        public static event Action<int> OnMaxHealthIncreased;
        public static event Action OnDied;

        private int _maxHealth = 100;

        public int Health { get; set; }

        [SerializeField] private AudioSource[] _soundsToPlayOnDamage;
        [SerializeField] private AudioSource[] _soundsToPlayOnDeath;

        [SerializeField] private ParticleSystem[] _particlesToPlayOnDamage;
        [SerializeField] private ParticleSystem[] _particlesToStopOnDeath;
        [SerializeField] private ParticleSystem[] _particlesToPlayOnDeath;

        private void Awake()
        {
            Health = _maxHealth;
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

                PlaySoundsOnDamage();
                PlayParticlesOnDamage();
            }
        }

        public virtual void PlaySoundsOnDamage()
        {
            for (int i = 0; i < _soundsToPlayOnDamage.Length; i++)
            {
                _soundsToPlayOnDamage[i].Play();
            }
        }

        public virtual void PlaySoundsOnDeath()
        {
            for (int i = 0; i < _soundsToPlayOnDeath.Length; i++)
            {
                _soundsToPlayOnDeath[i].Play();
            }
        }

        public virtual void PlayParticlesOnDamage()
        {
            for (int i = 0; i < _particlesToPlayOnDamage.Length; i++)
            {
                _particlesToPlayOnDamage[i].Play();
            }
        }

        public virtual void StopParticlesOnDeath()
        {
            for (int i = 0; i < _particlesToStopOnDeath.Length; i++)
            {
                _particlesToStopOnDeath[i].Stop();
            }
        }

        public virtual void PlayParticlesOnDeath()
        {
            for (int i = 0; i < _particlesToPlayOnDeath.Length; i++)
            {
                _particlesToPlayOnDeath[i].Play();
            }
        }

        public void Death()
        {
            // Reset health to 0 in case the city got hit at the end
            Health = 0;
            OnHealthChanged?.Invoke(Health);

            PlaySoundsOnDeath();
            StopParticlesOnDeath();
            PlayParticlesOnDeath();

            // The level is failed
            OnDied?.Invoke();
        }
    }
}