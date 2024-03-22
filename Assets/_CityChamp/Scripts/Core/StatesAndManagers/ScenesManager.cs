using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpectraStudios.CityChamp
{
    public class ScenesManager : MonoBehaviour
    {
        /*[SerializeField] private SceneField 

        private void Awake()
        {
            GameManager.OnGameStateChanged += ;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= ;
        }*/

        private void Start()
        {
            SceneManager.LoadScene("Level2", LoadSceneMode.Additive);
        }
    }
}
