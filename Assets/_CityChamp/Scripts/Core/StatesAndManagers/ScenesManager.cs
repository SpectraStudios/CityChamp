using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpectraStudios.CityChamp
{
    public class ScenesManager : MonoBehaviour
    {
        //[SerializeField] private SceneField;

        [SerializeField] private GameObject _levelPanel;
        [SerializeField] private GameObject _loadingPanel;
        [SerializeField] private Image _progressBar;

        private string _currentLevelScene;

        private static ScenesManager _instance;
        public static ScenesManager Instance
        {
            get
            {
                /*if (_instance == null)
                { Debug.LogError("Scenes Manager is NULL.");
                }*/
                return _instance;
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /*private void Awake()
        {
            GameManager.OnGameStateChanged += ;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= ;
        }*/

        private void Start()
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Additive);
            _currentLevelScene = "Level1";
        }


        private void ChangeScene(string sceneName)
        {

        }

        private void UnloadScene(string sceneName)
        {
            SceneManager.UnloadSceneAsync(_currentLevelScene);
        }

        private async void LoadScene(string sceneName)
        {
            var scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            scene.allowSceneActivation = false;

            _levelPanel.SetActive(false);
            _loadingPanel.SetActive(true);

            do
            {
                _progressBar.fillAmount = scene.progress;
            } while (scene.progress < 0.9f);

            scene.allowSceneActivation = true;
            _loadingPanel.SetActive(false);

            _currentLevelScene = sceneName;

            _levelPanel.SetActive(true);
        }

        public void GoToLevelAR()
        {
            if (_currentLevelScene != "AR")
            {
                UnloadScene(_currentLevelScene);
                LoadScene("AR");
            }
        }

        public void GoToLevel1()
        {
            if (_currentLevelScene != "Level1")
            {
                UnloadScene(_currentLevelScene);
                LoadScene("Level1");
            }
        }

        public void GoToLevel2()
        {
            if (_currentLevelScene != "Level2")
            {
                UnloadScene(_currentLevelScene);
                LoadScene("Level2");
            }
        }

        public void GoToLevel3()
        {
            if (_currentLevelScene != "Level3")
            {
                UnloadScene(_currentLevelScene);
                LoadScene("Level3");
            }
        }

        public void GoToLevel4()
        {
            if (_currentLevelScene != "Level4")
            {
                UnloadScene(_currentLevelScene);
                LoadScene("Level4");
            }
        }

        public void GoToLevel5()
        {
            if (_currentLevelScene != "Level5")
            {
                UnloadScene(_currentLevelScene);
                LoadScene("Level5");
            }
        }

        public void GoToLevel6()
        {
            if (_currentLevelScene != "Level6")
            {
                UnloadScene(_currentLevelScene);
                LoadScene("Level6");
            }
        }
    }
}
