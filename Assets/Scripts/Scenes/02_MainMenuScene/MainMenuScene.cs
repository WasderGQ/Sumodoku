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
        
        [SerializeField] private SO_GameMode _gameTypes;
        [SerializeField] private Button _startGame;
        [SerializeField] private Banner _banner;
        [SerializeField] private Button _exit;
        [SerializeField] private MainAnimationController _mainAnimationController;
        private void Start()
        {
            init();
        }
        
        void init() 
        {
            EventListener();
            SetVariable();
            InitVariable();
        }
        void SetVariable()
        {
            _banner = new Banner();
        }
        void InitVariable()
        {
            _banner.Init();
            _mainAnimationController.Init();
        }
        
        void EventListener()
        {
           
            _startGame.onClick.AddListener(StartGameMode9x9);
            _exit.onClick.AddListener(ExitGame);
        }

        private void OnDestroy()
        {
            _startGame.onClick.RemoveAllListeners();
            _exit.onClick.RemoveAllListeners();
        }
        private void StartGameMode3x3()
        {
            _gameTypes.SetGamemode3x3();
            SceneLoader.Instance.LoadScene(EnumScenes.GameSceneSudoku);
        }
        private void StartGameMode6x6()
        {
            _gameTypes.SetGamemode6x6();
            SceneLoader.Instance.LoadScene(EnumScenes.GameSceneSudoku);
        }
        private void StartGameMode9x9()
        {
            _gameTypes.SetGamemode9x9();
            _mainAnimationController.StopAnimation = true;
            SceneLoader.Instance.LoadScene(EnumScenes.GameSceneSudoku);
        }
        private void StartGameMode12x12()
        {
            _gameTypes.SetGamemode12x12();
            SceneLoader.Instance.LoadScene(EnumScenes.GameSceneSudoku);
        }
        private void ExitGame()
        {
            _mainAnimationController.StopAnimation = true;
            Application.Quit();
        }
    }
}
