using UnityEngine;
using WasderGQ.Sudoku.Enums;

namespace WasderGQ.Sudoku.Scenes.MainMenuScene
{
    [CreateAssetMenu(fileName = "GameMode", menuName = "ScriptableObjects/GameMode", order = 1)]
    public class SO_GameMode : ScriptableObject
    {

        [field: SerializeField] public GameBoards GameBoards { get; private set; }
        [field: SerializeField] public GameDifficulty GameDifficulty { get; private set; }

        public void SetGamemode(GameBoards gameBoards)
        {
            GameBoards = gameBoards;
        }

        public void SetGameDifficulty(GameDifficulty gameDifficulty)
        {
            GameDifficulty = gameDifficulty;
        }



    }
}

