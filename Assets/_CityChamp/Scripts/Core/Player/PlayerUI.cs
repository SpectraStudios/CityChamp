using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpectraStudios.CityChamp
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Slider _playerHealthSlider;
        [SerializeField] private Gradient _playerHealthGradient;
        [SerializeField] private Image _playerHealthFill;
        [SerializeField] private RectTransform _playerHealthRectTransform;

        private void Awake()
        {
            PlayerCombat.OnHealthChanged += UpdatePlayerHealthUI;
            PlayerCombat.OnMaxHealthIncreased += SetMaxPlayerHealthUI;
        }

        private void OnDestroy()
        {
            PlayerCombat.OnHealthChanged -= UpdatePlayerHealthUI;
            PlayerCombat.OnMaxHealthIncreased -= SetMaxPlayerHealthUI;
        }

        public void SetMaxPlayerHealthUI(int maxHealth)
        {
            _playerHealthRectTransform.sizeDelta = new Vector2(_playerHealthRectTransform.sizeDelta.x + 5, _playerHealthRectTransform.sizeDelta.y);
            _playerHealthSlider.maxValue = maxHealth;
        }

        public void UpdatePlayerHealthUI(int health)
        {
            _playerHealthSlider.value = health;
            _playerHealthFill.color = _playerHealthGradient.Evaluate(_playerHealthSlider.normalizedValue);
        }
    }
}
