using UnityEngine;
using WasderGQ.Sudoku.Enums;

namespace WasderGQ.Sudoku.Scenes.MainMenuScene
{
    [CreateAssetMenu(fileName = "GameMode", menuName = "ScriptableObjects/GameMode", order = 1)]
    public class SO_GameMode : ScriptableObject
    {

        [field:SerializeField]public GameBoards GameBoards { get; private set; }

        public void SetGamemode3x3()
        {
            GameBoards = GameBoards.x3;
        }
   
        public void SetGamemode6x6()
        {
            GameBoards = GameBoards.x6;
        }
    
        public void SetGamemode9x9() 
        {
            GameBoards = GameBoards.x9;
        }
    
        public void SetGamemode12x12()
        {
            GameBoards = GameBoards.x12;
        }
        

        private void GameStart()
        {
        
        
        
        

        }
        
    }
}

