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
        SelectMode,
        WorldMap,
        Scan,
        ARLevel,
        VRLevel,
        Win,
        Lose
    }

    public class GameManager : MonoBehaviour
    {
        public static event Action<GameMode> OnGameModeChanged;
        public static event Action<GameState> OnGameStateChanged;

        public GameMode CurrentGameMode { get; private set; }
        public GameState CurrentGameState { get; private set; }

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

        public void UpdateGameState(GameState newGameState)
        {
            CurrentGameState = newGameState;

            switch (newGameState)
            {
                case GameState.SelectMode:
                    break;
                case GameState.WorldMap:
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
            }

            OnGameStateChanged?.Invoke(newGameState);
        }
    }
}
