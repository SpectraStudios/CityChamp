using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.OVR.Scripts;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpectraStudios.CityChamp.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Slider _playerHealthSlider;
        [SerializeField] private Gradient _playerHealthGradient;
        [SerializeField] private Image _playerHealthFill;
        [SerializeField] private RectTransform _playerHealthRectTransform;
        [SerializeField] private Slider _cityCoreHealthSlider;
        [SerializeField] private Gradient _cityCoreHealthGradient;
        [SerializeField] private Image _cityCoreHealthFill;
        [SerializeField] private RectTransform _cityCoreHealthRectTransform;

        private void Awake()
        {
            Player.OnHealthChanged += UpdatePlayerHealthUI;
            Player.OnMaxHealthIncreased += SetMaxPlayerHealthUI;
            CityCore.OnHealthChanged += UpdateCityCoreHealthUI;
            CityCore.OnMaxHealthIncreased += SetMaxCityCoreHealthUI;
        }

        private void OnDestroy()
        {
            Player.OnHealthChanged -= UpdatePlayerHealthUI;
            Player.OnMaxHealthIncreased -= SetMaxPlayerHealthUI;
            CityCore.OnHealthChanged -= UpdateCityCoreHealthUI;
            CityCore.OnMaxHealthIncreased -= SetMaxCityCoreHealthUI;
        }

        public void SetMaxPlayerHealthUI(int maxHealth)
        {
            _playerHealthRectTransform.sizeDelta = new Vector2(_playerHealthRectTransform.sizeDelta.x + 5, _playerHealthRectTransform.sizeDelta.y);
            _playerHealthSlider.maxValue = maxHealth;
        }

        public void SetMaxCityCoreHealthUI(int maxHealth)
        {
            _cityCoreHealthRectTransform.sizeDelta = new Vector2(_cityCoreHealthRectTransform.sizeDelta.x + 5, _cityCoreHealthRectTransform.sizeDelta.y);
            _cityCoreHealthRectTransform.localPosition += new Vector3(-2.5f, 0, 0);
            _cityCoreHealthSlider.maxValue = maxHealth;
        }

        public void UpdatePlayerHealthUI(int health)
        {
            _playerHealthSlider.value = health;
            _playerHealthFill.color = _playerHealthGradient.Evaluate(_playerHealthSlider.normalizedValue);
        }

        public void UpdateCityCoreHealthUI(int health)
        {
            _cityCoreHealthSlider.value = health;
            _cityCoreHealthFill.color = _cityCoreHealthGradient.Evaluate(_cityCoreHealthSlider.normalizedValue);
        }
    }
}
