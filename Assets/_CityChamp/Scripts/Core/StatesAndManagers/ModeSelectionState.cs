using UnityEngine;

namespace SpectraStudios.CityChamp
{
    public class ModeSelectionState : MonoBehaviour
    {
        public void SelectOutdoorAR()
        {
            GameManager.Instance.UpdateGameMode(GameMode.OutdoorAR);
        }

        public void SelectIndoorVR()
        {
            GameManager.Instance.UpdateGameMode(GameMode.IndoorVR);
        }
    }
}
