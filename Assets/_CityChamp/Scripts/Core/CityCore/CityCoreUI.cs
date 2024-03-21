using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpectraStudios.CityChamp
{
    public class CityCoreUI : MonoBehaviour
    {
        [SerializeField] private Slider _cityCoreHealthSlider;
        [SerializeField] private Gradient _cityCoreHealthGradient;
        [SerializeField] private Image _cityCoreHealthFill;
        [SerializeField] private RectTransform _cityCoreHealthRectTransform;

        private void Awake()
        {
            CityCore.OnHealthChanged += UpdateCityCoreHealthUI;
            CityCore.OnMaxHealthIncreased += SetMaxCityCoreHealthUI;
        }

        private void OnDestroy()
        {
            CityCore.OnHealthChanged -= UpdateCityCoreHealthUI;
            CityCore.OnMaxHealthIncreased -= SetMaxCityCoreHealthUI;
        }

        public void SetMaxCityCoreHealthUI(int maxHealth)
        {
            _cityCoreHealthRectTransform.sizeDelta = new Vector2(_cityCoreHealthRectTransform.sizeDelta.x + 5, _cityCoreHealthRectTransform.sizeDelta.y);
            _cityCoreHealthRectTransform.localPosition += new Vector3(-2.5f, 0, 0);
            _cityCoreHealthSlider.maxValue = maxHealth;
        }

        public void UpdateCityCoreHealthUI(int health)
        {
            _cityCoreHealthSlider.value = health;
            _cityCoreHealthFill.color = _cityCoreHealthGradient.Evaluate(_cityCoreHealthSlider.normalizedValue);
        }
    }
}
