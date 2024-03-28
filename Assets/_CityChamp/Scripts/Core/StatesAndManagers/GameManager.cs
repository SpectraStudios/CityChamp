using System;
using UnityEngine;

namespace SpectraStudios.CityChamp
{
    public enum GameMode
    {
        OutdoorAR,
        IndoorVR
    }

    public enum GameState
    {
        Login,
        CreateAccount,
        ConnectWallet,
        SelectMode,
        MainMenu,
        Scan,
        ARLevel,
        VRLevel,
        Win,
        Lose,
        Loading
    }

    public class GameManager : MonoBehaviour
    {
        public static event Action<GameMode> OnGameModeChanged;
        public static event Action<GameState> OnGameStateChanged;

        public GameMode CurrentGameMode { get; private set; }
        public GameState CurrentGameState { get; private set; }

        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("Game Manager is NULL.");
                }
                return _instance;
            }
        }


        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            CurrentGameState = GameState.SelectMode;
        }

        public void UpdateGameMode(GameMode newGameMode)
        {
            CurrentGameMode = newGameMode;

            switch (newGameMode)
            {
                case GameMode.OutdoorAR:
                    break;
                case GameMode.IndoorVR:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newGameMode), newGameMode, null);
            }

            OnGameModeChanged?.Invoke(newGameMode);
        }

        public void SelectOutdoorAR()
        {
            UpdateGameMode(GameMode.OutdoorAR);
        }

        public void SelectIndoorVR()
        {
            UpdateGameMode(GameMode.IndoorVR);
        }

        public void UpdateGameState(GameState newGameState)
        {
            CurrentGameState = newGameState;

            switch (newGameState)
            {
                case GameState.Login:
                    break;
                case GameState.CreateAccount:
                    break;
                case GameState.SelectMode:
                    break;
                case GameState.MainMenu:
                    break;
                case GameState.Scan:
                    break;
                case GameState.ARLevel:
                    break;
                case GameState.VRLevel:
                    break;
                case GameState.Win:
                    break;
                case GameState.Lose:
                    break;
                case GameState.Loading:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
            }

            OnGameStateChanged?.Invoke(newGameState);
        }
    }
}
