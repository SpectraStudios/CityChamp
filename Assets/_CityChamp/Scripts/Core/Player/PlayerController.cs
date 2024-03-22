using System;
using UnityEngine;

namespace SpectraStudios.CityChamp
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject _playerCombat;

        private void Awake()
        {
            GameManager.OnGameStateChanged += ActivatePlayerComponents;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= ActivatePlayerComponents;
        }

        private void Start()
        {
            if (_playerCombat == null)
            {
                Debug.LogWarning("Player Combat components have not been assigned in the Inspector.");
            }
        }

        private void ActivatePlayerComponents(GameState currentGameState)
        {
            switch (currentGameState)
            {
                case GameState.SelectMode:
                    _playerCombat.SetActive(false);
                    break;
                case GameState.WorldMap:
                    _playerCombat.SetActive(false);
                    break;
                case GameState.Scan:
                    _playerCombat.SetActive(false);
                    break;
                case GameState.ARLevel:
                    _playerCombat.SetActive(true);
                    break;
                case GameState.VRLevel:
                    _playerCombat.SetActive(true);
                    break;
                case GameState.Win:
                    _playerCombat.SetActive(false);
                    break;
                case GameState.Lose:
                    _playerCombat.SetActive(false);
                    break;
                case GameState.Loading:
                    _playerCombat.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentGameState), currentGameState, null);
            }
        }
    }
}
