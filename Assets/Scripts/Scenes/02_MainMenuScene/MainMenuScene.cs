using DG.Tweening;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using WasderGQ.Sudoku.Enums;
using WasderGQ.Sudoku.SceneManagement;
using WasderGQ.Sudoku.Services.GoogleAds;

namespace WasderGQ.Sudoku.Scenes.MainMenuScene
{
    public class MainMenuScene : MonoBehaviour
    {
        
        [SerializeField] private SO_GameMode _gameModes;
        [SerializeField] private Button _startGame;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _exit;
        [SerializeField] private SettingController _settingsPanel;
        [SerializeField] private MainAnimationController _mainAnimationController;
        private void Start()
        {
            InIt();
        }
        
        async void InIt()
        {
            EventListener();
            InItVariable();
            await BannerController.Instance.Init();
        }
       
        void InItVariable()
        {
            _mainAnimationController.Init();
        }
        
        void EventListener()
        {
           
            _startGame.onClick.AddListener(StartGame);
            _settings.onClick.AddListener(OpenSettingsPanel);
            _exit.onClick.AddListener(ExitGame);
        }

        private void OnDestroy()
        {
            _startGame.onClick.RemoveAllListeners();
            _exit.onClick.RemoveAllListeners();
        }

        private void StartGame()
        {
            SceneLoader.Instance.WLoadScene(EnumScenes.GameSceneSudoku);
        }

        private void OpenSettingsPanel()
        {
            _settingsPanel.gameObject.SetActive(true);
            _settingsPanel.InIt();
        }
        private void ExitGame()
        {
            _mainAnimationController.StopAnimation = true;
            Application.Quit();
        }
    }
}
