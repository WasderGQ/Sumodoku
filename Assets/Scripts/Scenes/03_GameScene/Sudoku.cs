using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WasderGQ.Sudoku.Enums;
using WasderGQ.Sudoku.SceneManagement;
using WasderGQ.Sudoku.Scenes.GameScene.GameElement.Boards;
using WasderGQ.Sudoku.Scenes.GameScene.InputModuls;
using WasderGQ.Sudoku.Scenes.MainMenuScene;



namespace WasderGQ.Sudoku.Scenes.GameScene
{
    public class Sudoku : MonoBehaviour
    {
        [SerializeField] private SO_GameMode _gameMode;
        [SerializeField] private List<Board> _boardList;
        [SerializeField] private int _currentlySelectedBoard;
        [SerializeField] private Keyboard _keyboard;
        
        private void Start()
        {
            InIt();
        }
        

        private void ChangeHintBoolOnZones()
        {
            foreach (var parsel in _boardList[_currentlySelectedBoard].Parsels)
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
        }
        
        private void InIt()
        {
            GameBoardOpener();
            BoardInIt();
            KeyboardInIt();
        }
        
        private void BoardInIt()
        {
            _boardList[(int)_gameMode.GameBoards].InIt();
        }
        
        private void KeyboardInIt()
        {
            _keyboard.init();
        }
        
        private void GameBoardOpener()
        {
            _keyboard.gameObject.SetActive(true);
            _boardList[(int)_gameMode.GameBoards].gameObject.SetActive(true);
        }
        
        public void CheckWin()
        {
            foreach (var parsel in _boardList[(int)_gameMode.GameBoards].Parsels)
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
