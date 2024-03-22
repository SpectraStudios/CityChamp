using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
//using SpectraStudios.CityChamp.GameStates;

namespace SpectraStudios.CityChamp.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : PooledNavMeshAgent, IDamageable
    {
        public static event Action OnEnemyDied;
        public static event Action<int> OnEnemyDiedRedeemPoints;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private Collider _collider;
        [SerializeField] private Animator _animator;
        [SerializeField] private EnemyScriptableObject _enemyScriptableObject;
        public Slider EnemyHealthSlider;

        [SerializeField] private ParticleSystem[] _particlesToPlayForAttack1;
        [SerializeField] private ParticleSystem[] _particlesToPlayForAttack2;
        [SerializeField] private ParticleSystem[] _particlesToPlayForDefend;
        [SerializeField] private ParticleSystem[] _particlesToPlayOnDamage;
        [SerializeField] private ParticleSystem[] _particlesToPlayOnStun;
        [SerializeField] private ParticleSystem[] _particlesToStopOnDeath;
        [SerializeField] private ParticleSystem[] _particlesToPlayOnDeath;

        public int Health { get; set; }
        private int MaxHealth;

        private int _reformPoints;

        public bool IsDead { get; private set; }
        private float _deathAnimTime;

        [HideInInspector] public bool IsDefending;

        private void Awake()
        {
            //CombatState.OnWaveFailed += Death;

            SetUpHealthBar(GameObject.Find("EnemyHealthCanvas").GetComponent<Canvas>());
        }

        private void OnDestroy()
        {
            //CombatState.OnWaveFailed -= Death;
        }

        public virtual void OnEnable()
        {
            _agent.enabled = true;
            
            SetupAgentFromConfiguration();

            _rigidBody.isKinematic = false;
            _collider.enabled = true;

            // Set up the enemy's health each time it is spawned
            Health = MaxHealth;
            EnemyHealthSlider.gameObject.SetActive(true); 
            UpdateHealthUI(Health);
            
            IsDefending = false;

            // Reset dead status if it was recycled from the pool
            IsDead = false;
        }

        public virtual void SetupAgentFromConfiguration()
        {
            MaxHealth = _enemyScriptableObject.Health;
            _reformPoints = _enemyScriptableObject.ReformPoints;
            _deathAnimTime = _enemyScriptableObject.DeathAnimTime;

            _agent.speed = _enemyScriptableObject.Speed;
            _agent.angularSpeed = _enemyScriptableObject.AngularSpeed;
            _agent.acceleration = _enemyScriptableObject.Acceleration;
            _agent.stoppingDistance = _enemyScriptableObject.StoppingDistance;
            _agent.baseOffset = _enemyScriptableObject.BaseOffset;
            _agent.radius = _enemyScriptableObject.Radius;
            _agent.height = _enemyScriptableObject.Height;
            _agent.avoidancePriority = _enemyScriptableObject.AvoidancePriority;
            _agent.areaMask = _enemyScriptableObject.AreaMask;
            _agent.obstacleAvoidanceType = _enemyScriptableObject.ObstacleAvoidanceType;
        }

        public void TakeDamage(int damageAmount)
        {
            if (!IsDead && !IsDefending)
            {
                if ((Health - damageAmount) <= 0)
                {
                    Health = 0;
                    UpdateHealthUI(Health);
                    Death();
                }
                else
                {
                    Health -= damageAmount;
                    UpdateHealthUI(Health);

                    PlayParticlesOnDamage();
                }
            }
        }

        public void SetUpHealthBar(Canvas Canvas)
        {
            if (EnemyHealthSlider.transform.parent != Canvas.transform)
            {
                EnemyHealthSlider.transform.SetParent(Canvas.transform);
            }
        }

        public void UpdateHealthUI(int currentHealth)
        {
            EnemyHealthSlider.value = currentHealth;
        }

        public virtual void PlayParticlesOnDamage()
        {
            if (_particlesToPlayOnDamage != null)
            {
                for (int i = 0; i < _particlesToPlayOnDamage.Length; i++)
                {
                    _particlesToPlayOnDamage[i].Play();
                }
            }
        }

        public virtual void StopParticlesOnDeath()
        {
            if (_particlesToStopOnDeath != null)
            {
                for (int i = 0; i < _particlesToStopOnDeath.Length; i++)
                {
                    _particlesToStopOnDeath[i].Stop();
                }
            }
        }

        public virtual void PlayParticlesOnDeath()
        {
            if (_particlesToPlayOnDeath != null)
            {
                for (int i = 0; i < _particlesToPlayOnDeath.Length; i++)
                {
                    _particlesToPlayOnDeath[i].Play();
                }
            }       
        }

        public virtual void Death()
        {
            IsDead = true;

            _rigidBody.isKinematic = true;
            _collider.enabled = false;

            EnemyHealthSlider.gameObject.SetActive(value: false);

            StopParticlesOnDeath();
            PlayParticlesOnDeath();

            _animator.SetBool("isDying", true);

            Invoke("Disable", _deathAnimTime);
        }

        public virtual void Disable()
        {
            // Call events that handle point redemption before events that end the wave
            OnEnemyDiedRedeemPoints?.Invoke(_reformPoints);
            OnEnemyDied?.Invoke();

            _agent.enabled = false;
            gameObject.SetActive(false);
        }

        public override void OnDisable()
        {
            // Must turn off health bar on disable, otherwise they won't be turned off when the enemy pools are first instantiated (they are turned off without death)
            if (EnemyHealthSlider != null)
            {
                EnemyHealthSlider.gameObject.SetActive(value: false);
            }

            base.OnDisable();
        }
    }
}