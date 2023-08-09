using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WasderGQ.Sudoku.AIs;
using WasderGQ.Sudoku.Enums;
using WasderGQ.Sudoku.Generic;
using WasderGQ.Sudoku.SceneManagement;
using WasderGQ.Sudoku.Scenes.GameScene.Game.Boards;
using WasderGQ.Sudoku.Scenes.GameScene.InputModuls;
using WasderGQ.Sudoku.Scenes.MainMenuScene;
using WasderGQ.Utility.UnityEditor;


namespace WasderGQ.Sudoku.Scenes.GameScene
{
    public class Sudoku : MonoBehaviour
    {
        [SerializeField] private Button _hinter;
        [SerializeField] private SO_GameMode _gameMode;
        [SerializeField] private List<Boardx9> _boardList;
        [SerializeField] private SolvedSudokuCreater _solvedSudokuCreater;
        [SerializeField] private int _currentlySelectedBoard;
        [SerializeField] private Keyboard _keyboard;
        [SerializeField] private GameObject WaitScenePrefab;
        [SerializeField] private GameObject WaitScene;
        private void Start()
        {
            InIt();
            SetAddListener();
        }

        private void SetAddListener()
        {
            _hinter.onClick.AddListener(ChangeHintBoolOnZones);
        }

        private void ChangeHintBoolOnZones()
        {
            foreach (var parsel in  _boardList[_currentlySelectedBoard].Parsels)
            {
                foreach (var zone in parsel.ZonesInParsel)
                {
                    if (zone.IsHint)
                    {
                        zone.ChangeHintSetting(false);
                    }
                    else
                    {
                        zone.ChangeHintSetting(true);
                    }
                }
            }
            print(_boardList[_currentlySelectedBoard].Parsels[0].ZonesInParsel[0].IsHint);
        }
        
        
        
        private async void InIt()
        {
            GetCurrentBoard();
            GameBoardOpener();
            BoardInIt();
            KeyboardInIt();
        }
        
        private void BoardInIt()
        {
            _boardList[_currentlySelectedBoard].InIt();
        }
        
        private void KeyboardInIt()
        {
            _keyboard.init();
        }

        private void GetCurrentBoard()
        {
            _currentlySelectedBoard = (int)_gameMode.GameBoards;
        }

        private void GameBoardOpener()
        {
            _keyboard.gameObject.SetActive(true);
            _boardList[_currentlySelectedBoard].gameObject.SetActive(true);
        }
        
        private void StartWaitingSceneOnInItState()
        {
            WaitScene = Instantiate(WaitScenePrefab,Vector3.zero,Quaternion.identity,this.transform);
            
        }

        private void StopWaitingSceneOnInItState()
        {
            Destroy(WaitScene);
        }

        public void CheckWin()
        {
            foreach (var parsel in _boardList[_currentlySelectedBoard].Parsels)
            {
                foreach (var zone in parsel.ZonesInParsel)
                {
                    if(zone.MyValue != zone.TrueValue)
                        return;
                }
            }
            SceneLoader.Instance.LoadScene(EnumScenes.Celebration);
        }





    }
}
